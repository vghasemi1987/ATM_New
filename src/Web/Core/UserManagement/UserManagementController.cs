using ApplicationCommon;
using DomainContracts.BranchAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BranchAggregate;
using Infrastructure.Data.ApplicationUserAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Core.UserManagement.ViewModels;
using Web.Extensions;
using Web.Extensions.Attributes;
using Tools = ApplicationCommon.Tools;

namespace Web.Core.UserManagement
{
    [Authorize]
    [DisplayName("مدیریت کاربران")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserValidator<ApplicationUser> _userValidator;
        //private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;
        private readonly IFileService _fileService;
        //private readonly IOrganizationalChartService _organizationalChartService;
        private readonly IBranchRepository _branchRepository;

        

        public UserManagementController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment env,
            RoleManager<ApplicationRole> roleManager,
            IUserValidator<ApplicationUser> userValidator,
            IFileService fileService,
            //IEmailService emailService,
            //IOrganizationalChartService organizationalChartService,
            IBranchRepository branchRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userValidator = userValidator;
            _fileService = fileService;
            //_emailService = emailService;
            //_organizationalChartService = organizationalChartService;
            _branchRepository = branchRepository;
            _env = env;
        }

        [HttpGet]
        [Permission]
        [DisplayName("لیست نمایشی")]
        public IActionResult Index()
        {
            var search = new SearchViewModel()
            {
                //FromDate = DateTime.Now.ToPersianDateTime("yyyy/MM/dd"),
                //ToDate = DateTime.Now.ToPersianDateTime("yyyy/MM/dd"),
                BranchHeadList = new SelectList(_branchRepository.GetAllBranches("branchhead"), nameof(BranchDto.Id), nameof(BranchDto.Title))
            };
            return View(search);
        }

        [HttpGet]
        public IActionResult GetRecords(string models, SearchViewModel searchModel)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            var listwhere = _userManager.Users
                .Where(o => (User.IsInRole(RolesEnum.Developer.DescriptionAttr()) || User.IsInRole(RolesEnum.Accounting.DescriptionAttr()) || o.BranchHeadId.Equals(User.GetBranchHeadId())) &&
                (User.IsInRole(RolesEnum.Developer.DescriptionAttr()) || !o.ApplicationUserRoles.Any(x => x.RoleId == (int)RolesEnum.Developer)) &&
                (!User.IsInRole(RolesEnum.BranchHead.DescriptionAttr()) || o.BranchHeadId == User.GetBranchHeadId()));
            if (searchModel.BranchId != null)
            {
                listwhere = listwhere.Where(o => o.BranchId.Equals(searchModel.BranchId));
            }
            else
            {
                if (searchModel.BranchHeadId != null)
                {
                    listwhere = listwhere.Where(o => o.BranchHeadId.Equals(searchModel.BranchHeadId));
                }
            }
            if (searchModel.Name != null)
            {
                listwhere = listwhere.Where(o => o.Name.Contains(searchModel.Name));
            }
            if (searchModel.UserName != null)
            {
                listwhere = listwhere.Where(o => o.UserName.Contains(searchModel.UserName));
            }
            if (searchModel.FromDate != null)
            {
                listwhere = listwhere.Where(o => o.RegDateTime >= searchModel.FromDate.ToMiladiDate());
            }
            if (searchModel.ToDate != null)
            {
                listwhere = listwhere.Where(o => o.RegDateTime <= searchModel.ToDate.ToMiladiDate());
            }
            if (searchModel.LockState != null)
            {
                listwhere = listwhere.Where(o => o.LockoutEnabled.Equals(searchModel.LockState));
            }
            var list = listwhere.Select(o => new UserDetailViewModel
            {
                Id = o.Id,

                Picture = o.Picture ?? "no_image.png",
                UserName = o.UserName,
                Name = o.Name,
                BranchId = o.BranchId,
                BranchHeadId = o.BranchHeadId,
                BranchHeadCode = o.BranchHead.Code,
                BranchTitle = o.Branch.Title,
                BranchCode = o.Branch.Code,
                BranchHeadTitle = o.BranchHead.Title,
                RegDateTime = o.RegDateTime,
                Roles = string.Join(",", o.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).ToList()),
                LockoutState = o.LockoutEnd.HasValue,

            })
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);

            return Json(list);
        }

        [HttpPost]
        public async Task<JsonResult> ValidateUserName(string username, int userId)
        {
            var result = await _userValidator.ValidateAsync(
                _userManager, new ApplicationUser { UserName = username, Id = userId });
            return Json(result.Succeeded ? "true" : null);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpGet]
        [Permission]
        [DisplayName("مشاهده جزئیات")]
        public async Task<IActionResult> GetDetail(int? id)
        {
            var userData = new UserFormViewModel
            {
                RolesList = new SelectList(_roleManager.Roles.Where(o => o.Id > User.GetRoleId()).ToList(), nameof(ApplicationRole.Name), nameof(ApplicationRole.Name)),
                BranchList = new SelectList(_branchRepository.GetAllBranchHeadAndBranchSort(), nameof(BranchDto.Id), nameof(BranchDto.Title)),
                /*OrganizationChartList = new SelectList(_organizationalChartService.GetAll(), nameof(OrganizationalChart.Id),
                    nameof(OrganizationalChart.Title))*/
            };
            if (!id.HasValue)
            {
                return PartialView("_Detail", userData);
            }
            var user = await _userManager.Users
                .Where(o => o.Id.Equals(id))
                .Include(o => o.ApplicationUserRoles)
                .ThenInclude(o => o.ApplicationRole)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            userData.PersonnelCode = user.PersonnelCode;
            userData.FirstName = user.FirstName;
            userData.LastName = user.LastName;
            userData.PhoneNumber = user.PhoneNumber;
            userData.Picture = user.Picture;
            userData.UserId = user.Id;
            userData.BranchId = user.BranchId != null ? user.BranchId : user.BranchHeadId;
            userData.UserName = user.UserName;
            userData.SelectedRole = user.ApplicationUserRoles.FirstOrDefault()?.ApplicationRole?.Name;
            userData.LockState = !user.LockoutEnd.HasValue;
            userData.OrganizationChartId = user.OrganizationalChartId.GetValueOrDefault();
            return PartialView("_Detail", userData);
        }

        [HttpGet]
        [Permission]
        [DisplayName("ویرایش پروفایل")]
        public async Task<IActionResult> UpdateProfile(string id)
        {
            var user = await _userManager.Users
                .Where(o => o.UserName.Equals(id))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateProfileViewModel
            {
                Email = user.Email,
                //PersonnelCode = user.PersonnelCode,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Picture = user.Picture,
                UserId = user.Id,
                UserName = user.UserName,
                BranchId = user.BranchId,
                BranchHeadId = user.BranchHeadId,



                //OrganizationList = new SelectList(await _organizationalChartService.GetAllAsync(), nameof(OrganizationalChart.Id),
                //    nameof(OrganizationalChart.Title), user.OrganizationalChartId)
            };
            return PartialView("_UpdateProfile", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        [DisplayName("ثبت")]
        public async Task<IActionResult> AddDetail(UserFormViewModel model)
        {
            int? branchId = null;
            int? branchHeadId = null;
            if (model.BranchId != null)
            {
                var b = _branchRepository.GetBranchHeadIdById(model.BranchId.Value);
                if (b != null)
                {
                    branchId = model.BranchId.Value;
                    branchHeadId = b;
                }
                else
                {
                    branchId = null;
                    branchHeadId = model.BranchId.Value;
                }
            }

            if (model.PostedFile != null)
            {
                model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
                var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
                await _fileService.SaveFileToServerAsync(path, model.PostedFile);
            }

            var user = new ApplicationUser
            {
                PersonnelCode = model.PersonnelCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Picture = model.Picture,
                UserName = model.UserName,
                BranchId = branchId,
                BranchHeadId = branchHeadId,
                OrganizationalChartId = model.OrganizationChartId,
                RegisterByUserId = User.GetUserId(),
                LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return Json(new
                {
                    Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
                });
            }

            await _userManager.AddToRoleAsync(user, model.SelectedRole);
            return Json(new
            {
                Message = Message.Show("اطلاعات کاربر با موفقیت ثبت شد...", MessageType.Success),
                RefreshGrid = true
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission]
        [DisplayName("ویرایش")]
        public async Task<IActionResult> EditDetail(UserFormViewModel model)
        {
            int? branchId = null;
            int? branchHeadId = null;
            if (model.BranchId != null)
            {
                var b = _branchRepository.GetBranchHeadIdById(model.BranchId.Value);
                if (b != null)
                {
                    branchId = model.BranchId.Value;
                    branchHeadId = b;
                }
                else
                {
                    branchId = null;
                    branchHeadId = model.BranchId.Value;
                }
            }

            if (model.PostedFile != null)
            {
                model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
                var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
                await _fileService.SaveFileToServerAsync(path, model.PostedFile);
            }

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            user.UserName = model.UserName;
            user.PersonnelCode = model.PersonnelCode;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Picture = model.Picture;
            user.BranchId = branchId;
            user.BranchHeadId = branchHeadId;
            user.LockoutEnd = model.LockState.GetValueOrDefault() ? (DateTimeOffset?)null : DateTimeOffset.MaxValue;
            user.OrganizationalChartId = model.OrganizationChartId;
            if (!string.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return Json(new
                {
                    Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
                });
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
            await _userManager.AddToRoleAsync(user, model.SelectedRole);
            return Json(new
            {
                Message = Message.Show("اطلاعات کاربر با موفقیت ثبت شد...", MessageType.Success),
                RefreshGrid = true
            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            if (model.PostedFile != null)
            {
                model.Picture = model.Picture ?? Tools.NewFileName(model.PostedFile.FileName);
                var path = $"{_env.WebRootPath}\\uploads\\user-image\\{model.Picture}";
                await _fileService.SaveFileToServerAsync(path, model.PostedFile);
            }

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            //user.Email = model.Email;
            //if (model.Gender != null) user.Gender = model.Gender.Value;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Picture = model.Picture;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Json(new
                {
                    Message = Message.Show("اطلاعات شما با موفقیت ویرایش شد...", MessageType.Success),
                    RefreshGrid = true
                });
            AddErrors(result);
            return Json(new
            {
                Message = Message.Show(ModelState.GetErrors(), MessageType.Warning)
            });
        }

        [HttpPost]
        [Permission]
        [DisplayName("حذف")]
        public async Task<IActionResult> DeleteRows(List<int> ids)
        {
            if (!ids.Any()) return Json(false);
            //var pictureList = new List<string>();
            var usersList = _userManager.Users.Where(o => ids.Contains(o.Id));

            foreach (var user in usersList)
            {
                //await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
                //pictureList.Add(user.Picture);
                await _userManager.SetLockoutEnabledAsync(user, true);
            }

            //if (pictureList.Any())
            //{
            //    _fileService.DeleteFileListFromServer(pictureList, _env + @"uploads\user-image\");
            //}
            return Ok();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Signin(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        [ServiceFilter(typeof(UserActivityLogAttribute))]
        [DisplayName("ورود")]
        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            var user = _userManager.Users
                .Include(o => o.ApplicationUserRoles)
                .Include(o => o.Branch)
                .Include(o => o.BranchHead)
                .FirstOrDefault(o => o.UserName.Equals(model.UserName));
            if (user == null)
            {
                return Json(new { Message = Message.Show("امکان ورود به سامانه برای شما مقدور نمی باشد.", MessageType.Error) });
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ViewData["UserId"] = user.Id;
                return Json(new
                {
                    Redirect = Url.IsLocalUrl(model.ReturnUrl) && !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : Url.Action("Index", "Home"),
                    Success = true,
                    PanelMenu = user.ApplicationUserRoles.FirstOrDefault()?.ApplicationRole.PanelMenu
                });
            }
            if (result.IsLockedOut)
            {
                return Json(new
                {
                    Redirect = Url.Action(nameof(Lockout)),
                    Success = true
                });
            }

            ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور نادرست است!");
            return Json(new { Message = Message.Show(ModelState.GetErrors(), MessageType.Error) });
        }

        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Signin), "UserManagement");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) /*|| !(await UserManager.IsEmailConfirmedAsync(user.Id)))*/
            {
                return Json(Message.Show("پست الکترونیکی وارد شده یافت نشد!", MessageType.Error));
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //await _emailService.SendResetPasswordAsync(user.Email, Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme));
            return Json(Message.Show("برای تغییر رمز عبور، پست الکترونیکی خور را بررسی کنید...", MessageType.Success));
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Json(Message.Show("پست الکترونیکی یافت نشد!", MessageType.Error));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return Json(new
                {
                    Message = Message.Show("رمز عبور با موفقیت تغییر یافت"),
                    Redirect = Url.Action("Signin", "UserManagement"),
                    Success = true
                });
            }
            AddErrors(result);
            return Json(Message.Show(ModelState.GetErrors(), MessageType.Warning));
        }

        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            user.PasswordUpdate = DateTime.Now;

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            //var userEdit = await _userManager.FindByIdAsync(user.Id.ToString());
            //userEdit.PasswordUpdate = DateTime.Now;
            if (!result.Succeeded)
            {
                AddErrors(result);
                return Json(new
                {
                    Message = Message.Show("رمز عبور فعلی رو به درستی وارد ننمودید", MessageType.Warning)
                });
            }
            var resultPasswordUpdate = await _userManager.UpdateAsync(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Json(new
            {
                Message = Message.Show("رمز عبور شما با موفقیت تغییر کرد...", MessageType.Success),
                Success = true
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(SignIn));
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [Permission]
        [DisplayName("ورود به عنوان")]
        public async Task<IActionResult> ImpersonateUser()
        {
            var users = (await _userManager.Users.ToListAsync()).Where(o => o.Id != User.GetUserId());
            var model = new ImpersonateUserViewModel
            {
                Users = new SelectList(users,
                    nameof(ApplicationUser.Id), nameof(ApplicationUser.Name))
            };
            return PartialView("_ImpersonateUser", model);
        }
        [HttpPost]
        [Permission]
        public async Task<IActionResult> ImpersonateUser(ImpersonateUserViewModel model)
        {
            var currentUserId = User.GetUserId();
            var impersonatedUser = await _userManager.FindByIdAsync(model.UserId);
            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);
            userPrincipal.Identities.First().AddClaim(new Claim("OriginalUserId", currentUserId.ToString()));
            userPrincipal.Identities.First().AddClaim(new Claim("IsImpersonating", "true"));
            await _signInManager.SignOutAsync();
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);
            return Json(new { Redirect = Url.Action(nameof(Index), "Home") });
        }

        public async Task<IActionResult> StopImpersonation()
        {
            if (!User.IsImpersonating())
            {
                throw new Exception("You are not impersonating now. Can't stop impersonation");
            }
            var originalUserId = User.FindFirst("OriginalUserId").Value;
            var originalUser = await _userManager.FindByIdAsync(originalUserId);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(originalUser, isPersistent: true);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UsersList()
        {
            var users = await _userManager.Users
                .Select(o => new { o.Name, o.UserName, Picture = o.Picture ?? "no_image.png" })
                .AsNoTracking()
                .ToArrayAsync();
            return Json(users.Select(user => $"{user.UserName},{user.Name},{user.Picture}").ToList());
        }
       
    }
}