using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using System;

namespace DomainEntities.HamrahLoan
{
    public class HamrahLoanDetail : Entity<int>
    {
        public HamrahLoanHeader Header { get; set; }
        public int HeaderId { get; set; }
        public HamrahLoanStatus Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? StatusDate { get; set; }
        public int? UserChangeStatusId { get; set; }
        public ApplicationUser UserChangeStatus { get; set; }
        public string LoanNumber { get; set; }
        public long Amount { get; set; }
        public DateTime LoanDate { get; set; }
        public int FolowNumber { get; set; }

    }
    public enum HamrahLoanStatus : byte
    {
        //درحال بررسی
        Pending = 1,
        //بررسی شده
        Reviewed = 2,
        //عدم مغایرت
        NoDiscrepancy=3,
        //شناسه در سما یافت نشد
        NoSama=4,
        //شناسه در همراه یافت نشد
        NoHamrah=5,
        //مغایرت دارد توسط کاربر
        DiscrepancyByUser = 6,
        //عدم مغایرت توسط کاربر
        NoDiscrepancyByUser = 7,
    }
}
