using DomainEntities.NotificationAggregate;

namespace DomainApplication.Specifications
{
    public sealed class NotificationFilterSpecification : BaseSpecification<NotificationItem>
    {
        public NotificationFilterSpecification(bool isSent)
            : base(o => o.IsSent == isSent)
        {
            AddInclude(o=>o.Category);
        }
    }
}