namespace DomainEntities.BranchAggregate
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BranchHead { get; set; }
        public int Code { get; set; }
        public int? BranchHeadCode { get; set; }
        public int? BranchHeadId { get; set; }
    }
}
