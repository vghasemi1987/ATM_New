
using DomainEntities.Commons;

namespace DomainEntities.ReportAggregate
{
    public class Chart : Entity<int>
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public string SeriesName { get; set; }
        public string ClassName { get; set; }
        public string Style { get; set; }
        public string Color { get; set; }
        public string ChartOptions { get; set; }
        public string AccessRight { get; set; }
    }
}