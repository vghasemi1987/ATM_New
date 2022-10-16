using System.Collections.Generic;

namespace DomainEntities.ReportAggregate
{
    public class ChartItemDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string SeriesName { get; set; }
        public List<object> Data { get; set; }
        public string ClassName { get; set; }
        public string Style { get; set; }
        public string Color { get; set; }
        public string ChartOptions { get; set; }
        public string AccessRight { get; set; }
        public List<string> Categories { get; set; }
    }
}
