using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainContracts.BranchAggregate;
using DomainContracts.ReportAggregate;
using DomainEntities.ApplicationUserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Core.Reports.ModelViews;
using Web.Extensions;

namespace Web.Core.Reports
{
    [Authorize]
    [DisplayName("گزارشات")]
    public class ReportsController : Controller
    {
        private readonly IReportBoxRepository _reportRepository;
        private readonly IChartReportService _chartReportService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBranchRepository _branchRepository;

        public ReportsController(IReportBoxRepository reportRepository,
            IChartReportService chartReportService,
            UserManager<ApplicationUser> userManager,
            IBranchRepository branchRepository)
        {
            _reportRepository = reportRepository;
            _chartReportService = chartReportService;
            _userManager = userManager;
            _branchRepository = branchRepository;
        }

        [DisplayName("صفحه اصلی")]
        public IActionResult Index()
        {
            //ViewData["Message"] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var model = new DateRangeViewModel();
            model.BranchHeadSelectList = new SelectList( _branchRepository.GetAllBranches("branchhead")
                    ,"Id", "Title");
            return View(model);
        }

        public async Task<IActionResult> ReportBox(DateRangeViewModel model)
        {
            const string dateFrm = "yyyy/MM/dd";
            var fromDate = $"'{model.FromEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MinValue.ToString(dateFrm)}'";
            var toDate = $"'{model.ToEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MaxValue.ToString(dateFrm)}'";
            var branchHeadId = (model.BranchHeadId == null ? 0 : model.BranchHeadId).ToString();
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
            var list = await _reportRepository.GetByRoleReportBoxes((await _userManager.GetRolesAsync(user)).FirstOrDefault());
            var dataDictionary = list.ToDictionary(item => item.Key, item => (dynamic)0);
            dataDictionary = await _reportRepository.ExecuteCommand(string.Join(" ",
                list.Select(o => 
                    o.SqlCommand
                    .Replace("#UserId", User.GetUserId().ToString())
                    .Replace("#picker1", fromDate)
                    .Replace("#picker2", toDate)
                    .Replace("#picker3", branchHeadId)
                )), dataDictionary);

            foreach (var (key, value) in dataDictionary)
            {
                var obj = list.FirstOrDefault(x => x.Key == key);
                if (obj != null) obj.Value = value.ToString();
            }

            var boxList = list.Select(o =>
                new DetailViewModel
                {
                    BoxStatus = o.BoxStatus,
                    Icon = o.Icon,
                    Link = o.Link
                    .Replace("#picker1", model.FromEntryDate ?? "")
                    .Replace("#picker2", model.ToEntryDate ?? "")
                    .Replace("#picker3", model.BranchHeadId.ToString() ?? ""),
                    Title = o.Title,
                    Value = o.Value,
                    Description = o.Description
                }).ToList();
            return PartialView("_ReportBox", boxList);
        }

        public async Task<IActionResult> ChartData(DateRangeViewModel model)
        {
            const string dateFrm = "yyyy/MM/dd";
            var fromDate = $"'{model.FromEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MinValue.ToString(dateFrm)}'";
            var toDate = $"'{model.ToEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MaxValue.ToString(dateFrm)}'";
            var userId = User.GetUserId().ToString();
            var user = await _userManager.FindByIdAsync(userId);
            var chartsData = await _chartReportService.GetChartData((await _userManager.GetRolesAsync(user)).FirstOrDefault()
                , fromDate, toDate, userId,model.BranchHeadId.ToString() ?? "0");
            return Json(chartsData);
        }
    }
}