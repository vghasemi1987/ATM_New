using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.HamrahLoan;
using DomainEntities.HamrahLoan;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Core.HamrahLoanHeaders.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.HamrahLoanHeaders
{
    [Authorize]
    [DisplayName("لیست تسهیلات همراه")]
    public class HamrahLoanHeadersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHamrahLoanHeaderRepository _headerRepository;
        private readonly IHamrahLoanDetailRepository _detailRepository;
        public HamrahLoanHeadersController(
            IUnitOfWork unitOfWork,
            IHamrahLoanHeaderRepository headerRepository,
            IHamrahLoanDetailRepository detailRepository)
        {
            _unitOfWork = unitOfWork;
            _headerRepository = headerRepository;
            _detailRepository = detailRepository;
        }

        [Permission]
        [DisplayName("لیست")]
        public IActionResult Index()
        {
            return View();
        }

        [Permission]
        [DisplayName("لیست جزئیات")]
        public IActionResult IndexDetail(int headerId)
        {
            return View(headerId);
        }
        //[Permission]
        [DisplayName("گزارش")]
        public IActionResult IndexReport()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            int? branchId = User.GetBranchId();
            var list = _headerRepository.GetList(request, branchId.Value);
            return Json(list);
        }


        [HttpGet]
        public IActionResult GetRecordsDetail(string models, int? headerId)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            int? branchId = User.GetBranchId();
            if (headerId>0)
            {
                var list = _detailRepository.GetList(request, headerId.Value, branchId.Value);
                return Json(list);
            }
            else
            {
                var list = _detailRepository.GetList(request, branchId.Value,false);
                return Json(list);
            }

        }
        [HttpGet]
        public IActionResult GetRecordsReport(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            int? branchId = User.GetBranchId();
            if (User.IsInRole(RolesEnum.AdminHamrahLoan.DescriptionAttr()))
            {
                var list = _detailRepository.GetList(request, branchId.Value,true);
                return Json(list);
            }
            else
            {
                var list = _detailRepository.GetList(request, branchId.Value,false);
                return Json(list);
            }

        }
        [HttpGet]
        [Permission]
        [DisplayName("ارسال فایل")]
        public IActionResult GetDetail()
        {
            var model = new HamrahLoanHeaderViewModel();
            return PartialView("_Detail", model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        //[Permission]
        [DisplayName("افزودن")]
        public async Task<IActionResult> AddDetail(HamrahLoanHeaderViewModel model)
        {
            using (var stream = model.PostedFile.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)model.PostedFile.Length);

                    int? branchId = User.GetBranchId();
                    if (!(branchId > 0))
                    {
                        return Json(new
                        {
                            Message = Message.Show("شعبه کاربر شما مشخص نشده است، لطفا با اداره حسابداری تماس حاصل فرمایید", MessageType.Warning),
                            RefreshGrid = true
                        });
                    }
                    var header = new HamrahLoanHeader
                    {
                        BranchId = branchId.Value,
                        Status = HamrahLoanStatus.Pending,
                        Title = model.Title,
                        UserCreateId = User.GetUserId(),
                    };
                    using (var reader = new StreamReader(model.PostedFile.OpenReadStream()))
                    {
                        var details = new List<HamrahLoanDetail>();
                        var stringFile = reader.ReadToEnd();
                        var lineStringFile = Regex.Split(stringFile, "\r\n|\r|\n");
                        var i = 0;
                        foreach (var item in lineStringFile)
                        {
                            i++;
                            try
                            {
                                var splitLine = item.Split(',');

                                if (splitLine.Length == 4)
                                {
                                    var detail = new HamrahLoanDetail
                                    {
                                        Amount = Int64.Parse(splitLine[1]),
                                        LoanDate = (splitLine[2].Substring(0, 4) + "/" + splitLine[2].Substring(4, 2) + "/" + splitLine[2].Substring(6, 2)).ToMiladiDate(),
                                        LoanNumber = splitLine[0],
                                        FolowNumber = Int32.Parse(splitLine[3]),
                                        Status = HamrahLoanStatus.Pending
                                    };
                                    details.Add(detail);
                                }
                            }
                            catch (Exception e)
                            {
                                return Json(new
                                {
                                    Message = Message.Show($"خط {i} مشکل دارد بررسی نمایید", MessageType.Warning),
                                    RefreshGrid = true
                                });
                            }

                        }
                        header.Details = details;
                        _headerRepository.Add(header);
                        await _unitOfWork.SaveAsync();
                    }
                    return Json(new
                    {
                        Message = Message.Show($"فایل با موفقیت ارسال شد", MessageType.Success),
                        RefreshGrid = true
                    });
                }
            }
        }

        [HttpDelete]
        [Permission]
        [DisplayName("حذف")]
        public async Task<IActionResult> DeleteRow(int? id)
        {
            if (!id.HasValue) return Json(false);
            _headerRepository.Delete(new HamrahLoanHeader { Id = id.Value });
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("رکورد انتخابی با موفقیت حذف شد ...", MessageType.Success),
                RefreshGrid = true
            });
        }
        [HttpGet]
        [Permission]
        [DisplayName("تغییر وضعیت")]
        public IActionResult DetailChangeStatus(int id, HamrahLoanStatus statusId)
        {
            if (statusId == HamrahLoanStatus.Pending)
            {
                var model = new ChangeStatusViewModel
                {
                    Id = 0,
                    Warning = "پس از بررسی قابلیت تغییر وجود دارد"
                };
                return PartialView("_DetailChangeStatus", model);

            }
            if (statusId == HamrahLoanStatus.NoDiscrepancy)
            {
                var model = new ChangeStatusViewModel
                {
                    Id = 0,
                    Warning = "مغایرت وجود ندارد"
                };
                return PartialView("_DetailChangeStatus", model);
            }
            else
            {
                var model = new ChangeStatusViewModel
                {
                    Id = id
                };
                return PartialView("_DetailChangeStatus", model);
            }


        }
        [HttpPost]
        [DisplayName("تغییر وضعیت")]
        public async Task<IActionResult> DetailChangeStatus(ChangeStatusViewModel model)
        {
            var entity = new HamrahLoanDetail
            {
                Id = model.Id,
                Status = model.Status,
                StatusDate = DateTime.Now,
                UserChangeStatusId = User.GetUserId()
            };
            _detailRepository.Update(entity,
                o => o.Status,
                o => o.StatusDate,
                o => o.UserChangeStatusId);
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("وضعیت با موفقیت تغییر نمود...", MessageType.Success),
                RefreshGrid = true
            });
        }
        [HttpGet]
        public IActionResult GetRecordsStatusHeader()
        {
            var list = new List<object>();
            list.Add(new { Value = 1, Text = "درحال بررسی" });
            list.Add(new { Value = 2, Text = "بررسی شده" });

            return Json(list);
        }
        [HttpGet]
        public IActionResult GetRecordsStatusDetail()
        {
            var list = new List<object>();
            list.Add(new { Value = 1, Text = "درحال بررسی" });
            list.Add(new { Value = 3, Text = "عدم مغایرت" });
            list.Add(new { Value = 4, Text = "شناسه در سما یافت نشد" });
            list.Add(new { Value = 5, Text = "شناسه در همراه یافت نشد" });
            list.Add(new { Value = 6, Text = "مغایرت دارد توسط کاربر" });
            list.Add(new { Value = 7, Text = "عدم مغایرت توسط کاربر" });
            return Json(list);
        }
    }
}