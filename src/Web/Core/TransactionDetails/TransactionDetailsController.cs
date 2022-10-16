using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.TransactionAggregate;
using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using Infrastructure.Data.TransactionFileAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Web.Core.TransactionDetails.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.TransactionDetails
{
    [Authorize]
    [DisplayName("مدیریت لیست تراکنش ها")]
    public class TransactionDetailsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileDetailRepository _fileDetailRepository;
        private readonly TransactionFileRepository _transactionFileRepository;

        public TransactionDetailsController(IUnitOfWork unitOfWork,
            IFileDetailRepository fileDetailRepository,
            ITransactionService transactionService,
            TransactionFileRepository transactionFileRepository)
        {
            _unitOfWork = unitOfWork;
            _fileDetailRepository = fileDetailRepository;
            _transactionService = transactionService;
            _transactionFileRepository = transactionFileRepository;
        }
        [Permission]
        [DisplayName("لیست تراکنش ها")]
        public IActionResult Index(int? id)
        {
            return View(id);
        }

        //ToDo: Use Ui for Filter
        [HttpGet]
        public IActionResult GetRecords(string models, string filter, int? fileId)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            var advanceFilter = new AdvancedFilter();
            if (!string.IsNullOrEmpty(filter))
                advanceFilter = JsonConvert.DeserializeObject<AdvancedFilter>(filter);

            var branchId = !User.IsInRole(RolesEnum.Accounting.DescriptionAttr()) ? User.GetBranchId() : (int?)null;
            if (User.IsInRole(RolesEnum.BranchBoss.DescriptionAttr()))
            {
                return Json(_fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                    new List<EnumStatus?> { EnumStatus.BackToBranchBoss, EnumStatus.SendToBranchBoss }));
            }
            if (User.IsInRole(RolesEnum.Operator.DescriptionAttr()))
            {
                var r= _fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                    new List<EnumStatus?> { EnumStatus.BackToOperator, EnumStatus.OperatorProcessing });
                return Json(r);
            }
            else
            {
                /*var ttt = _fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                    new List<EnumStatus> { EnumStatus.SendToAccounting, EnumStatus.FinalRegistration });*/
                return Json(_fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                    new List<EnumStatus?> { EnumStatus.SendToAccounting, EnumStatus.FinalRegistration }));
            }
            
        }

        [HttpGet]
        public IActionResult GetRecordsAll(string models, string filter, int? fileId)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            var advanceFilter = new AdvancedFilter();
            if (!string.IsNullOrEmpty(filter))
                advanceFilter = JsonConvert.DeserializeObject<AdvancedFilter>(filter);

            var branchId = !User.IsInRole(RolesEnum.Accounting.DescriptionAttr()) ? User.GetBranchId() : (int?)null;

            return Json(_fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                new List<EnumStatus?> 
                {
                    EnumStatus.BackToBranchBoss,
                    EnumStatus.SendToBranchBoss,
                    EnumStatus.BackToOperator,
                    EnumStatus.SendToAccounting,
                    EnumStatus.FinalRegistration,
                    EnumStatus.OperatorProcessing,
                }));



        }

        [Permission]
        [DisplayName("دریافت فایل شتابی")]
        public IActionResult Shetabi()
        {
            return View(new SearchViewModel { SearchType = SearchType.ShetabiSearch });
        }

        [HttpGet]
        public IActionResult GetShetabiRecords(string models, SearchViewModel searchModel)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            
            var list = _fileDetailRepository.GetShetabiFileList(request,
                string.IsNullOrEmpty(searchModel.FromDate)?(int?)null: int.Parse(searchModel.FromDate.Replace("/", string.Empty)),
                string.IsNullOrEmpty(searchModel.ToDate) ? (int?)null : int.Parse(searchModel.ToDate.Replace("/", string.Empty)));
            return Json(list);
        }

        [Permission]
        [DisplayName("سند برداشت تجمعی")]
        public IActionResult BranchTransaction()
        {
            return View(new SearchViewModel { SearchType = SearchType.SumOfBranch });
        }

        [HttpGet]
        public IActionResult GetSumBranchTransaction(string models, SearchViewModel searchModel)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            var list = _fileDetailRepository.GetSumBranchTransaction(request,
                string.IsNullOrEmpty(searchModel.FromDate) ? (int?)null : int.Parse(searchModel.FromDate.Replace("/", string.Empty)),
                string.IsNullOrEmpty(searchModel.ToDate) ? (int?)null : int.Parse(searchModel.ToDate.Replace("/", string.Empty)));
            return Json(list);
        }

        [HttpGet]
        public IActionResult SendToState(ToNextStepViewModel model)
        {
            return PartialView("_SendToState", model);
        }

        [HttpPost]
        public IActionResult SubmitToState(ToNextStepViewModel model)
        {

            var r=_transactionService.SendToStatus(model.Ids.ToList(), model.Status, model.MessageText, User.GetUserId());
            if (r=="ok")
            {
                return Json(new
                {
                    Message = Message.Show("اطلاعات با موفقیت ارسال شد...", MessageType.Success),
                    RefreshGrid = true
                });
            }
            else if (r == "DateBigSiXMonth")
            {
                return Json(new
                {
                    Message = Message.Show("از تاریخ تراکنش 6 ماه گذشته است...", MessageType.Warning),
                    RefreshGrid = true
                });
            }
            else 
            {
                return Json(new
                {
                    Message = Message.Show("در داخل تراکنش ها کارت رفاهی موجود است...", MessageType.Warning),
                    RefreshGrid = true
                });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetShetabiFile(string ids)
        {
            var fileByte = await _transactionService.GetShetabiFile(JsonConvert.DeserializeObject<List<int>>(ids));
            return new FileContentResult(fileByte, new MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = "ShetabFile.txt"
            };
        }

        [HttpGet]
        public async Task<IActionResult> GenerateExcelReport(string ids)
        {
            var filePath = await _transactionService.GenerateExcelReport(JsonConvert.DeserializeObject<List<int>>(ids));
            filePath.Position = 0;
            return new FileStreamResult(filePath, new MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = "ExcelExport.xlsx"
            };
        }

        [HttpDelete]
        [Permission]
        [DisplayName("حذف تراکنش")]
        public async Task<IActionResult> DeleteRows(List<int> ids)
        {
            if (!ids.Any()) return Json(false);
            foreach (var item in ids)
            {
                _fileDetailRepository.Delete(new FileDetail { Id = item });
            }
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("رکورد های انتخابی با موفقیت حذف شدند ...", MessageType.Success),
                RefreshGrid = true
            });
        }

        public IActionResult Search(SearchViewModel model)
        {
            var search = new SearchViewModel()
            {
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                SearchType = model.SearchType
            };
            return PartialView("_Search", search);
        }

        [Permission]
        [DisplayName("لیست تراکنش های شعبه")]
        public IActionResult BranchDetail()
        {
            return View();
        }

        //ToDo: Use Ui for Filter
        [HttpGet]
        public IActionResult BranchDetailGetRecords(string models, string filter, int? fileId)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            var advanceFilter = new AdvancedFilter();

            if (!string.IsNullOrEmpty(filter))
                advanceFilter = JsonConvert.DeserializeObject<AdvancedFilter>(filter);

            var branchId = !User.IsInRole(RolesEnum.Accounting.DescriptionAttr()) ? User.GetBranchId() : (int?)null;

            var r = _fileDetailRepository.GetList(request, branchId, fileId, advanceFilter,
                new List<EnumStatus?> {
                    EnumStatus.BackToBranchBoss,
                    EnumStatus.SendToBranchBoss,
                    EnumStatus.BackToOperator,
                    EnumStatus.OperatorProcessing,
                    EnumStatus.SendToAccounting,
                    EnumStatus.FinalRegistration,
                });
            return Json(r);


        }
        /// <summary>
        /// جهت خروجی اکسل
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                var data = await _transactionFileRepository.GenerateExcelReport();
               
                data.Position = 0;
                return new FileStreamResult(data, System.Net.Mime.MediaTypeNames.Application.Octet)
                {
                    FileDownloadName = $"Export_{System.Guid.NewGuid().ToString().Substring(0, 5)}.xlsx"
                };
            }
            catch (System.Exception ex)
            {
                throw;
            }
          
        }
    }
}