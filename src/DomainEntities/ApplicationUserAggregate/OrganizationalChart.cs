using DomainEntities.Commons;

namespace DomainEntities.ApplicationUserAggregate
{
    public class OrganizationalChart : Entity<short>
    {
        public string Title { get; set; }
    }
}