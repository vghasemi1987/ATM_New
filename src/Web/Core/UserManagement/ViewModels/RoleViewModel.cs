using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Web.Core.AccessRights;
using Web.Extensions.ControllerDiscovery;

namespace Web.Core.UserManagement.ViewModels
{
    public class RoleViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "عنوان"), Required(ErrorMessage = "{0} را وارد نمایید"),
         Remote(nameof(AccessRightsController.ValidateRole), "AccessRights", AdditionalFields = nameof(Id),
        HttpMethod = "Post", ErrorMessage = "{0} وارد شده تکراری است")]
        public string Name { get; set; }

        public IEnumerable<ControllerInfo> ControllerInfos { get; set; }
        [Display(Name = "دسترسی ها")]
        public List<string> SelectedAccess { get; set; } = new List<string>();

        public string JsonMenu { get; set; }
    }
}
