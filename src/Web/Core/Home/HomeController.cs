using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainContracts.ReportAggregate;
using DomainEntities.ApplicationUserAggregate;
using Infrastructure.Data.Commons;
using Infrastructure.Data.TransactionFileAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Core.Reports.ModelViews;
using Web.Extensions;
using Web.Extensions.Attributes;

namespace Web.Core.Home
{
	[Authorize]
	[DisplayName("خانه")]
	public class HomeController : Controller
	{
		private readonly IReportBoxRepository _reportRepository;
		private readonly IChartReportService _chartReportService;
		private readonly UserManager<ApplicationUser> _userManager;
		public HomeController(IReportBoxRepository reportRepository,
			IChartReportService chartReportService,
			UserManager<ApplicationUser> userManager)
		{
			_reportRepository = reportRepository;
			_chartReportService = chartReportService;
			_userManager = userManager;
		}

		[ServiceFilter(typeof(UserActivityLogAttribute))]
		[DisplayName("صفحه اصلی")]
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user.PasswordUpdate == null)
			{
				return View("Index", "ChangePassword");
				//return RedirectToAction("ChangePassword", "UserManagement");
			}
			/*else if ((DateTime.Now - user.PasswordUpdate.Value).TotalDays > 90)
            {
                return View("Index","ChangePassword");
                //return RedirectToAction("ChangePassword", "UserManagement");

            }*/

			//ViewData["Message"] = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
			return View("Index", "");
		}
		public async Task<IActionResult> ReportBox(DateRangeViewModel model)
		{
			const string dateFrm = "yyyy/MM/dd";
			var fromDate = $"'{model.FromEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MinValue.ToString(dateFrm)}'";
			var toDate = $"'{model.ToEntryDate?.ToMiladiDate().ToString(dateFrm) ?? DateTime.MaxValue.ToString(dateFrm)}'";

			var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
			var list = await _reportRepository.GetByRoleReportBoxes((await _userManager.GetRolesAsync(user)).FirstOrDefault());
			var dataDictionary = list.ToDictionary(item => item.Key, item => (dynamic)0);
			dataDictionary = await _reportRepository.ExecuteCommand(string.Join(" ",
				list.Select(o =>
					o.SqlCommand.Replace("#UserId", User.GetUserId().ToString()).Replace("#picker1", fromDate).Replace("#picker2", toDate)
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
					Link = o.Link.Replace("#picker1", model.FromEntryDate ?? "").Replace("#picker2", model.ToEntryDate ?? ""),
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
			var chartsData = await _chartReportService.GetChartData((await _userManager.GetRolesAsync(user)).FirstOrDefault(), fromDate, toDate, userId, "");
			return Json(chartsData);
		}
	}
}