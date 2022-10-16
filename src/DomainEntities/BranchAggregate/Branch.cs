using System.Collections.Generic;
using DomainEntities.Commons;

namespace DomainEntities.BranchAggregate
{
    public class Branch: Entity<int>
    {
        public int? BranchHeadId { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public Branch BranchHead { get; set; }
        public IList<Branch> SubBranches { get; set; }
    }
}
