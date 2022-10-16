using DomainEntities.BranchAggregate;
using DomainEntities.Commons;

namespace DomainEntities.AccountAggregate
{
    public class Account : Entity<int>
    {
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public int? AccountNo { get; set; }
    }
}