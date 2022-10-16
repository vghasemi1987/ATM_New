using System;

namespace DomainEntities.HamrahLoan
{
    public class HamrahLoanHeaderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HamrahLoanStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StatusDate { get; set; }
        public int DetailsCount { get; set; }
        public string UserCreate { get; set; }
        public string BranchTitle { get; set; }
        public int BranchCode { get; set; }
    }
}
