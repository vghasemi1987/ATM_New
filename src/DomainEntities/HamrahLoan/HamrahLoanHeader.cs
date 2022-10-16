using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BranchAggregate;
using DomainEntities.Commons;
using System;
using System.Collections.Generic;

namespace DomainEntities.HamrahLoan
{
    public class HamrahLoanHeader : Entity<int>
    {
        public string Title { get; set; }
        public HamrahLoanStatus Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? StatusDate { get; set; }
        public ICollection<HamrahLoanDetail> Details { get; set; } = new List<HamrahLoanDetail>();
        public int UserCreateId { get; set; }
        public ApplicationUser UserCreate { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
