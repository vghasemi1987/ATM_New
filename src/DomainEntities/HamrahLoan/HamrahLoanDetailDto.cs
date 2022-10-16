using System;

namespace DomainEntities.HamrahLoan
{
    public class HamrahLoanDetailDto
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public HamrahLoanStatus Status { get; set; }
        public HamrahLoanStatus StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StatusDate { get; set; }
        public string UserChangeStatus { get; set; }
        public string LoanNumber { get; set; }
        public long Amount { get; set; }
        public DateTime LoanDate { get; set; }
        public int FolowNumber { get; set; }
        public string BranchTitle { get; set; }
    }
}
