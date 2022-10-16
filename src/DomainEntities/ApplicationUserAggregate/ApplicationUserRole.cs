using Microsoft.AspNetCore.Identity;

namespace DomainEntities.ApplicationUserAggregate
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ApplicationRole ApplicationRole { get; set; }
    }
}
