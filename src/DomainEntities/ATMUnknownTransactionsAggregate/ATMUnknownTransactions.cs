using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using System;
using System.Collections.Generic;

namespace DomainEntities.ATMUnknownTransactionsAggregate
{
    public class ATMUnknownTransactions : Entity<int>
    {
        public int? ATMUnknownTransactionsID { get; set; }
        public int? TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionTime { get; set; } //10
        public string TransactionNumber { get; set; } //20
        public string CardNumber { get; set; } //25
        public DateTime DeterminationDate { get; set; }
        public int? ATMID { get; set; }
        public string ATM { get; set; } //200
        public string Branch { get; set; } //200
        public int? BranchID { get; set; }
        public string BranchCode { get; set; } //20
        public string TransactionDateShamsi { get; set; } //10
        public string ATMAtletCode { get; set; }//20
        public string TransactionDateShamsi2 { get; set; }//10
        public string CardType { get; set; }//50
        public int? ATMDailyJournalTransactionsID { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }//50
        public int? ATMUnkhownTransactionDailyStatusID { get; set; }
        public bool? AfterCutover { get; set; }
        public int? SuccessfullTransactionID { get; set; }
        public bool? ATMHasJournal { get; set; }
        public string MaskCardNumber { get; set; }//30
        public int? DailyConflictID { get; set; }
        public bool? IsManually { get; set; }
        public DateTime ManualResolveDate { get; set; }


        public EnumStatus? StatusWorkfollowId { get; set; }
        public Status StatusWorkfollow { get; set; }
        public List<WorkfollowATMUnknownTransactions> Workfollows { get; set; } = new List<WorkfollowATMUnknownTransactions>();
        public string UserDescription { get; set; }
    }

}
