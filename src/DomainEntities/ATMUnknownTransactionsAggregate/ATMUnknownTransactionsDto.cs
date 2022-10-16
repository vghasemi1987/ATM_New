using DomainEntities.TransactionFileAggregate;

namespace DomainEntities.ATMUnknownTransactionsAggregate
{
    public class ATMUnknownTransactionsDto
    {
        public int Id { get; set; }
        public int? ATMUnknownTransactionsID { get; set; }
        public int? TransactionAmount { get; set; }//مبلغ تراکنش
        public string TransactionDate { get; set; }//تاریخ تراکنش
        public string TransactionTime { get; set; } //ساعت تراکنش
        public string TransactionNumber { get; set; } //شماره تراکنش
        public string CardNumber { get; set; } //شماره  کارت
        public string DeterminationDate { get; set; }
        public int? ATMID { get; set; }
        public string ATM { get; set; }//شماره سریال دستگاه
        public string Branch { get; set; } //200
        //public int? BranchID { get; set; }
        public string BranchCode { get; set; } //کد شعبه
        //public string TransactionDateShamsi { get; set; } //10
        public string ATMAtletCode { get; set; }//شماره اتلت دستگاه

        //public string TransactionDateShamsi2 { get; set; }//10
        public string CardType { get; set; }//نوع کارت
        //public int? ATMDailyJournalTransactionsID { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }//50
        //public int? ATMUnkhownTransactionDailyStatusID { get; set; }
        //public bool? AfterCutover { get; set; }
        //public int? SuccessfullTransactionID { get; set; }
        //public bool? ATMHasJournal { get; set; }
        //public string MaskCardNumber { get; set; }//30
        //public int? DailyConflictID { get; set; }
        //public bool? IsManually { get; set; }
        //public string ManualResolveDate { get; set; }
        public EnumStatus? StatusWorkfollowId { get; set; }
        public string StatusWorkfollowTitle { get; set; }
        public string UserDescription { get; set; }
    }
}
