using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainEntities.ApplicationUserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier) == null ? 0 : int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        public static string GetUserFullName(this ClaimsPrincipal principal)
        {
            return principal == null ? string.Empty : principal.FindFirst(ClaimTypes.GivenName)?.Value;
        }
        public static string GetUserPicture(this ClaimsPrincipal principal)
        {
            return principal == null ? string.Empty : principal.FindFirst(ClaimTypes.UserData)?.Value;
        }
        public static string GetBranchHeadName(this ClaimsPrincipal principal)
        {
            return principal == null ? string.Empty : principal.FindFirst("BranchHeadName")?.Value;
        }
        public static string GetBranchName(this ClaimsPrincipal principal)
        {
            return principal == null ? string.Empty : principal.FindFirst("BranchName")?.Value;
        }
        public static int GetBranchId(this ClaimsPrincipal principal)
        {
            var branchId = principal.FindFirst("BranchId")?.Value;
            return string.IsNullOrEmpty(branchId) ? 0 : int.Parse(branchId);
        }
        public static int GetBranchHeadId(this ClaimsPrincipal principal)
        {
            var branchHeadId = principal.FindFirst("BranchHeadId")?.Value;
            return string.IsNullOrEmpty(branchHeadId) ? 0 : int.Parse(branchHeadId);
        }
        
        public static int? GetRoleId(this ClaimsPrincipal principal)
        {
            var roleId = principal.FindFirst("RoleId")?.Value;
            return string.IsNullOrEmpty(roleId) ? (int?)null : int.Parse(roleId);
        }

        public static bool IsImpersonating(this ClaimsPrincipal principal)
        {
            return principal != null && principal.HasClaim("IsImpersonating", "true");
        }
    }
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public UserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.Name ?? string.Empty),
                new Claim(ClaimTypes.UserData, user.Picture ?? "no_image.png"),
                new Claim("BranchId", user.BranchId.HasValue? user.BranchId.Value.ToString():string.Empty),
                new Claim("BranchHeadId", user.BranchHeadId.HasValue? user.BranchHeadId.Value.ToString():string.Empty),
                new Claim("RoleId", user.ApplicationUserRoles?.FirstOrDefault()?.RoleId.ToString() ?? string.Empty),
                new Claim("BranchHeadName", user.BranchHead?.Title ?? string.Empty),
                new Claim("BranchName", user.Branch?.Title ?? string.Empty),
            });
            return identity;
        }
    }
}