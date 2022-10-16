using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainContracts.Commons;
using DomainContracts.ReportAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.ReportAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Web.Core.ReportBoxes.ModelViews;

namespace Web.Core.ReportBoxes
{
    [Authorize]
    //[DisplayName("مدیریت گزارش ها")]
    public class ReportBoxesController : Controller
    {
        private readonly IReportBoxRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ReportBoxesController(IUnitOfWork unitOfWork,
            IReportBoxRepository reportRepository,
            RoleManager<ApplicationRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = reportRepository;
            _roleManager = roleManager;
        }
        //[Permission("Report_Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            var list = _reportRepository.ListAll();
            return Json(list.AsQueryable().ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
        }

        [HttpGet]
        //[Permission("Report_GetBoxDetail")]
        public IActionResult GetBoxDetail(int? id)
        {
            var rolesList = new SelectList(_roleManager.Roles.ToList(), nameof(ApplicationRole.Name), nameof(ApplicationRole.Name));
            var model = new FormViewModel
            {
                Roles = rolesList
            };
            if (id.HasValue)
            {
                var oData = _reportRepository.GetSingleBySpec(o=>o.Id.Equals(id.Value));
                model = new FormViewModel
                {
                    Id = oData.Id,
                    Title = oData.Title,
                    Description = oData.Description,
                    AccessRight = oData.AccessRight.Split(",").ToList(),
                    BoxStatus = (BoxStatus) Enum.Parse(typeof(BoxStatus),
                        Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(oData.BoxStatus)),
                    Icon = oData.Icon,
                    Key = oData.Key,
                    Link = oData.Link,
                    SqlCommand = oData.SqlCommand,
                    Roles = rolesList
                };
            }
            return PartialView("_Detail", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[Permission("Report_AddDetail")]
        public async Task<IActionResult> AddDetail(ReportBox model)
        {
            var reportBox = new ReportBox
            {
                Key = model.Key,
                Link = model.Link,
                SqlCommand = model.SqlCommand,
                AccessRight = model.AccessRight,
                BoxStatus = model.BoxStatus,
                Icon = model.Icon,
                Title = model.Title,
                Description = model.Description
            };
            _reportRepository.Add(reportBox);
            await _unitOfWork.SaveAsync();

            return Json(new
            {
                Message = Message.Show("اطلاعات با موفقیت ثبت شد...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        //[Permission("Report_EditDetail")]
        public async Task<IActionResult> EditDetail(FormViewModel model)
        {
            //var repBox = _reportRepository.GetSingleBySpec(o=>o.Id.Equals(model.Id));
            var repBox = new ReportBox
            {
                Id = model.Id,
                AccessRight = string.Join(",", model.AccessRight),
                BoxStatus = model.BoxStatus.ToString().ToLower(),
                Icon = model.Icon,
                Key = model.Key,
                Link = model.Link,
                SqlCommand = model.SqlCommand,
                Title = model.Title,
                Description = model.Description
            };
            _reportRepository.Update(repBox);
            await _unitOfWork.SaveAsync();
            return Json(new
            {
                Message = Message.Show("اطلاعات با موفقیت ویرایش شد...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpPost]
        //[Permission("Report_DeleteRows")]
        public virtual async Task<IActionResult> DeleteRows(int? id)
        {
            if (!id.HasValue) return Json(false);
            _reportRepository.Delete(new ReportBox { Id = id.Value });
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}