using ApplicationCommon;
using DomainContracts.ATMUnknownTransactionsAggregate;
using DomainContracts.BranchAggregate;
using DomainContracts.Commons;
using DomainEntities.TransactionFileAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Web.Core.ATMUnknown.ViewModels;
using Web.Extensions;

namespace Web.Core.ATMUnknown
{
    [Authorize]
    [DisplayName("مدیریت لیست تراکنش ها بدون ژورنال")]
    public class ATMUnknownController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IATMUnknownTransactionsRepository _unknownTransactionsRepository;
        private readonly IATMUnknownTransactionsService _unknownTransactionsService;
        private readonly IBranchRepository _branchRepository;

        public ATMUnknownController(IUnitOfWork unitOfWork,
            IATMUnknownTransactionsRepository unknownTransactionsRepository,
            IATMUnknownTransactionsService unknownTransactionsService,
            IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _unknownTransactionsRepository = unknownTransactionsRepository;
            _branchRepository = branchRepository;
            _unknownTransactionsService = unknownTransactionsService;
        }

        //[Permission]
        [DisplayName("لیست تراکنش ها بلاتکلیف")]
        public IActionResult Index()
        {
            return View();
        }

        //ToDo: Use Ui for Filter
        [HttpGet]
        public IActionResult GetRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            /* var advanceFilter = new AdvancedFilter();
             if (!string.IsNullOrEmpty(filter))
                 advanceFilter = JsonConvert.DeserializeObject<AdvancedFilter>(filter);*/

            var branchId = !User.IsInRole(RolesEnum.Accounting.DescriptionAttr()) ? User.GetBranchId() : (int?)null;
            var branchcode = _branchRepository.GetBranchCode(branchId.Value);
            if (User.IsInRole(RolesEnum.BranchBoss.DescriptionAttr()))
            {
                return Json(_unknownTransactionsRepository.GetList(request, branchcode, //advanceFilter,
                    new List<EnumStatus?> { EnumStatus.BackToBranchBoss, EnumStatus.SendToBranchBoss }));
            }
            if (User.IsInRole(RolesEnum.Operator.DescriptionAttr()))
            {
                var r = _unknownTransactionsRepository.GetList(request, branchcode,// advanceFilter,
                    new List<EnumStatus?> { EnumStatus.BackToOperator, EnumStatus.OperatorProcessing });
                return Json(r);
            }
            else
            {
                var ttt = _unknownTransactionsRepository.GetList(request, branchcode, //advanceFilter,
                    new List<EnumStatus?> { EnumStatus.SendToAccounting, EnumStatus.FinalRegistration });
                return Json(_unknownTransactionsRepository.GetList(request, branchcode,// advanceFilter,
                    new List<EnumStatus?> { EnumStatus.SendToAccounting, EnumStatus.FinalRegistration }));
            }

        }

        [DisplayName("گزارش تراکنش ها بلاتکلیف")]
        public IActionResult IndexReport()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRecordsReport(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            List<string> branchcodes = new List<string>();
            if (User.IsInRole(RolesEnum.AdminATMUnknown.DescriptionAttr()) || User.IsInRole(RolesEnum.Accounting.DescriptionAttr()))
            {
                return Json(_unknownTransactionsRepository.GetList(request, branchcodes, true));
            }
            else if (User.IsInRole(RolesEnum.BranchHead.DescriptionAttr()))
            {
                var branchHeadId = User.GetBranchHeadId();
                if (branchHeadId > 0)
                {
                    branchcodes.AddRange(_branchRepository.GetBranchCodesByHeadId(branchHeadId));
                }
                return Json(_unknownTransactionsRepository.GetList(request, branchcodes, false));
            }
            else
            {
                var branchId = User.GetBranchId();
                if (branchId > 0)
                {
                    branchcodes.Add(_branchRepository.GetBranchCode(branchId));
                }
                return Json(_unknownTransactionsRepository.GetList(request, branchcodes, false));
            }
        }
        [HttpGet]
        public IActionResult SendToState(ToNextStepViewModel model)
        {
            return PartialView("_SendToState", model);
        }

        [HttpGet]
        public IActionResult GetListWorkfollows(int id)
        {
            var model = new ListWorkfollowViewModel
            {
                Workfollows = _unknownTransactionsRepository.GetListWorkfollows(id)
            };
            return PartialView("_Workfollows", model);
        }


        [HttpPost]
        public IActionResult SubmitToState(ToNextStepViewModel model)
        {
            _unknownTransactionsService.SendToStatus(model.Ids.ToList(), model.Status, model.MessageText, User.GetUserId(), model.CustomerOrBreanchId);
            return Json(new
            {
                Message = Message.Show("اطلاعات با موفقیت ارسال شد...", MessageType.Success),
                RefreshGrid = true
            });
        }
    }
}