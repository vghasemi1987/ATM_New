using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DomainEntities.ApplicationUserAggregate
{
    public sealed class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName) : this()
        {
            Name = roleName;
        }

        public string PanelMenu { get; set; }
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
