using DomainEntities.Commons;

namespace DomainEntities.NotificationAggregate
{
    public class Category : Entity<short>
    {
        public string Title { get; set; }
    }
}