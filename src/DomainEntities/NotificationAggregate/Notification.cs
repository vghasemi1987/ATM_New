using System;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.Commons;

namespace DomainEntities.NotificationAggregate
{
    public class NotificationItem : Entity<int>
    {
        public NotificationItem()
        {
            EntryDateTime = DateTime.Now;
            IsRead = false;
        }
        public DateTime EntryDateTime { get; private set; }
        public string Text { get; set; }
        public int CreatedByUserId { get; set; }
        public int ForUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public ApplicationUser ForUser { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        public short CategoryId { get; set; }
        public Category Category { get; set; }
        public string Url { get; set; }
    }
}