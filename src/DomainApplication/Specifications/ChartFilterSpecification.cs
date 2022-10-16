using DomainEntities.ReportAggregate;

namespace DomainApplication.Specifications
{
    public sealed class ChartFilterSpecification : BaseSpecification<Chart>
    {
        public ChartFilterSpecification(string role)
            : base(o => o.AccessRight.Contains(role))
        {
        }
    }
}