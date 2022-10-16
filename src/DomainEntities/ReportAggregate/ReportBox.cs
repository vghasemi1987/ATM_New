using DomainEntities.Commons;

namespace DomainEntities.ReportAggregate
{
    public class ReportBox: Entity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Link { get; set; }
        public string AccessRight { get; set; }
        public string BoxStatus { get; set; }
        public string SqlCommand { get; set; }
    }
}
