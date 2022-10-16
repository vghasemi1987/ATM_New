using System;
using DomainEntities.Commons;

namespace DomainEntities.ApplicationUserAggregate
{
    public class ApplicationUserActivity : Entity<int>
    {
        public ApplicationUserActivity()
        {
            EntryDateTime = DateTime.Now;
        }
        public string ActivityTitle { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime EntryDateTime { get; private set; }
    }
}