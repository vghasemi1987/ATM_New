using DomainEntities.BranchAggregate;
using System;

namespace Web.Core.UserManagement.ViewModels
{
    public class UserDetailViewModel
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime RegDateTime { get; set; }
        public string Roles { get; set; }
        public DateTime? DisabledDate { get; set; }
        public bool LockoutState { get; set; }
        public string DepartmentName { get; set; }
        public string OrganizationalChart { get; set; }
        public int? BranchId { get; set; }
        public string BranchTitle { get; set; }
        public int? BranchCode { get; set; }
        public int? BranchHeadId { get; set; }
        public string BranchHeadTitle { get; set; }
        public int? BranchHeadCode { get; set; }

    }
}
