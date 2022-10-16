using Microsoft.AspNetCore.Mvc;
using Web.Core.UserManagement;

namespace Web.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(UserManagementController.ConfirmEmail),
                controller: "UserManagement",
                values: new {userId, code},
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(UserManagementController.ResetPassword),
                controller: "UserManagement",
                values: new {userId, code},
                protocol: scheme);
        }
    }
}