using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainEntities.ApplicationUserAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Core.UserManagement.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;
using Web.Extensions.ControllerDiscovery;

namespace Web.Core.AccessRights
{
    [Authorize]
    [DisplayName("مدیریت دسترسی ها")]
    public class AccessRightsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRoleValidator<ApplicationRole> _roleValidator;
        private readonly ControllerDiscovery _controllerDiscovery;

        public AccessRightsController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IRoleValidator<ApplicationRole> roleValidator,
            ControllerDiscovery controllerDiscovery)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleValidator = roleValidator;
            _controllerDiscovery = controllerDiscovery;
        }

        [Permission]
        [DisplayName("لیست نمایشی")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RoleRecords(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);

            var list = _roleManager.Roles.AsQueryable();

            return Json(list.AsQueryable().ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
        }

        [HttpPost]
        public async Task<JsonResult> ValidateRole(string name, int id)
        {
            var result = await _roleValidator.ValidateAsync(_roleManager, new ApplicationRole { Id = id, Name = name });
            return Json(result.Succeeded);
        }

        [HttpGet]
        [Permission]
        [DisplayName("افزودن")]
        public IActionResult AddDetail()
        {
            var model = new RoleViewModel
            {
                ControllerInfos = _controllerDiscovery.GetControllers(),
                JsonMenu = GetDefaultMenu()
            };
            return PartialView("_Detail", model);
        }

        [HttpGet]
        [Permission]
        [DisplayName("مشاهده جزئیات")]
        public async Task<IActionResult> GetDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var oData = await _roleManager.FindByIdAsync(id.ToString());
            var model = new RoleViewModel
            {
                ControllerInfos = _controllerDiscovery.GetControllers(),
                Name = oData.Name,
                Id = oData.Id,
                SelectedAccess = (await _roleManager.GetClaimsAsync(oData)).Select(o => o.Value).ToList(),
                JsonMenu = oData.PanelMenu
            };
            return PartialView("_Detail", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        [DisplayName("افزودن")]
        public async Task<IActionResult> AddDetail(RoleViewModel model)
        {
            var result = await _roleManager.CreateAsync(new ApplicationRole { Name = model.Name, PanelMenu=model.JsonMenu });
            if (result.Succeeded)
            {
                foreach (var item in model.SelectedAccess)
                {
                    await _roleManager.AddClaimAsync(await _roleManager.FindByNameAsync(model.Name),
                        new Claim("Permission", item));
                }

                //await _signInManager.SignInAsync(await _userManager.GetUserAsync(User), false);
                return Json(new
                {
                    Message = Message.Show("اطلاعات نقش با موفقیت ویرایش شد...", MessageType.Success),
                    RefreshGrid = true
                });
            }

            AddErrors(result);
            return Json(new
            {
                Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [DisplayName("ویرایش")]
        [Permission]
        public async Task<IActionResult> EditDetail(RoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            role.Name = model.Name;
            role.PanelMenu = model.JsonMenu;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                var roleClaimList = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in roleClaimList)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                foreach (var item in model.SelectedAccess)
                {
                    await _roleManager.AddClaimAsync(role, new Claim("Permission", item));
                }

                await _signInManager.SignInAsync(await _userManager.GetUserAsync(User), false);
                return Json(new
                {
                    Message = Message.Show("اطلاعات نقش با موفقیت ویرایش شد...", MessageType.Success),
                    RefreshGrid = true
                });
            }

            AddErrors(result);
            return Json(new
            {
                Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
            });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpPost]
        [Permission]
        [DisplayName("حذف")]
        public async Task<IActionResult> DeleteRows(List<int> ids)
        {
            if (!ids.Any()) return Json(false);
            await _roleManager.DeleteAsync(new ApplicationRole { Id = ids[0] });
            return Ok();
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //[Permission]
        //[DisplayName("افزودن")]
        //public async Task<IActionResult> Edit(RoleViewModel model)
        //{
        //    var result = await _roleManager.CreateAsync(new ApplicationRole(model.Name));
        //    if (result.Succeeded)
        //    {
        //        foreach (var item in model.SelectedAccess)
        //        {
        //            await _roleManager.AddClaimAsync(await _roleManager.FindByNameAsync(model.Name),
        //                new Claim("Permission", item));
        //        }

        //        //await _signInManager.SignInAsync(await _userManager.GetUserAsync(User), false);
        //        return Json(new
        //        {
        //            Message = Message.Show("اطلاعات نقش با موفقیت ویرایش شد...", MessageType.Success),
        //            RefreshGrid = true
        //        });
        //    }

        //    AddErrors(result);
        //    return Json(new
        //    {
        //        Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
        //    });
        //}

        private string GetDefaultMenu()
        {
            return JsonConvert.SerializeObject(new List<UserPanelMenu>
            {
                new UserPanelMenu { Text = "خانه", Icon = "flaticon-line-graph", Link = "/home", Items = null },
                new UserPanelMenu { Text = "مدیریت کاربران", Icon = "flaticon-piggy-bank", Link = "/usermanagement", Items = null },
                new UserPanelMenu { Text = "آزمون نفوذ", Icon = "flaticon-signs-2", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "آزمون های نفوذ", Link = "/PenetrationTestItems", Items = null },
                    new UserPanelMenu { Text = "موارد آزمون", Link = "/penetrationtestcases", Items = null },
                    new UserPanelMenu { Text = "مدیریت سیستم ها", Link = "/penetrationapplicationsystems", Items = null }
                    }
                },
                new UserPanelMenu { Text = "آزمون کاربری", Icon = "fas fa-sitemap", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "آزمون های کاربری",Link = "/usertestitems", Items = null },
                    new UserPanelMenu { Text = "آزمون های من",Link = "/assignedusertests", Items = null },
                    new UserPanelMenu { Text = "موارد آزمون",Link = "/usertestcases", Items = null },
                    new UserPanelMenu { Text = "مدیریت گروه های آزمون", Link = "/usertestcasecreators", Items = null },
                    new UserPanelMenu { Text = "مدیریت سیستم ها", Link = "/usertestapplicationsystems", Items = null }
                    }
                },
                new UserPanelMenu { Text = "وظایف", Icon = "flaticon-list-3", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "تمامی وظایف",Link = "/todotask", Items = null },
                    new UserPanelMenu { Text = "وظایف من",Link = "/todotask?AssignedToUserId=1&oper=And&ToDoTaskStateId=1&ToDoTaskStateId=2&ToDoTaskStateId=4", Items = null },
                    new UserPanelMenu { Text = "وظایف بازگشتی",Link = "/todotask?CreatorUserId=1&oper=and&ToDoTaskStateId=4", Items = null },
                    new UserPanelMenu { Text = "وظایف انجام شده من",Link = "/todotask?AssignedToUserId=1&oper=and&ToDoTaskStateId=3", Items = null },
                    new UserPanelMenu { Text = "وظایف تعریف شده توسط من",Link = "/todotask?CreatorUserId=1&oper=and", Items = null },
                    new UserPanelMenu { Text = "جستجوی پیشرفته",Link = "/todotask/search", Items = null }
                    }
                },
                new UserPanelMenu { Text = "گزارشات", Icon = "flaticon-diagram", Link = "/reports", Items = null },
                new UserPanelMenu { Text = "تنظیمات", Icon = "fas fa-cogs", Items = new List<UserPanelMenu>{
                    new UserPanelMenu { Text = "پست الکترونیکی",Link = "/setting/email", Items = null },
                    new UserPanelMenu { Text = "سامانه",Link = "/setting/application", Items = null },
                    new UserPanelMenu { Text = "دپارتمان ها",Link = "/departments", Items = null },
                    new UserPanelMenu { Text = "لیست شرکت ها",Link = "/companies", Items = null },
                    new UserPanelMenu { Text = "اعضای شرکت ها",Link = "/companymembers", Items = null },
                    new UserPanelMenu { Text = "سطوح دسترسی", Link = "/accessrights", Items = null },
                    }
                }
            });
        }
    }
}