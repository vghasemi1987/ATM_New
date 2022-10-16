using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using System;

namespace DomainEntities.WorkfollowAggregate
{
    public class Workfollow : Entity<int>
    {
        public Workfollow()
        {
            UpdateDateTime = DateTime.Now;
        }
        public int? FileDetailId { get; set; }
        public EnumStatus? StatusId { get; set; }
        public FileDetail FileDetail { get; set; }
        public Status Status { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int? UserId { get; set; }
        //public ApplicationUser User { get; set; }
    }
}