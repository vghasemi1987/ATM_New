using DomainEntities.Commons;

namespace DomainEntities.SettingAggregate
{
    public class Priority : Entity<short>
    {
        public string Title { get; set; }
    }
}