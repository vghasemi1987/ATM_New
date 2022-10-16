using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using System;

namespace DomainEntities.ATMUnknownTransactionsAggregate
{
    public class WorkfollowATMUnknownTransactions : Entity<int>
    {
        public WorkfollowATMUnknownTransactions()
        {
            CreateDateTime = DateTime.Now;
        }
        public int? ATMUnknownTransactionsId { get; set; }
        public ATMUnknownTransactions ATMUnknownTransactions { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public short? StatusWorkfollowId { get; set; }
        public Status StatusWorkfollow { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }//50
        public string UserMessage { get; set; }
    }
}
