using System;
using System.Collections.Generic;
using DomainEntities.BranchAggregate;
using Microsoft.AspNetCore.Identity;

namespace DomainEntities.ApplicationUserAggregate
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            RegDateTime = DateTime.Now;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonnelCode { get; set; }
        public string Picture { get; set; }
        public DateTime RegDateTime { get; private set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public int? BranchHeadId { get; set; }
        public Branch BranchHead{ get; set; }
        public short? OrganizationalChartId { get; set; }
        public string Name => FirstName + " " + LastName;
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public int? RegisterByUserId { get; set; }
        public ApplicationUser RegisterByUser { get; set; }
        public DateTime? PasswordUpdate { get; set; }
    }
}
