using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCommon;
using DomainApplication.Specifications;
using DomainContracts.Commons;
using DomainContracts.ReportAggregate;
using DomainEntities.ReportAggregate;

namespace DomainApplication.Services.ReportAggregate
{
    public class ChartReportService : IChartReportService
    {
        private readonly IAsyncRepository<Chart> _repository;
        private readonly IReportBoxRepository _reportRepository;

        public ChartReportService(IAsyncRepository<Chart> repository,
            IReportBoxRepository reportRepository)
        {
            _repository = repository;
            _reportRepository = reportRepository;
        }

        public async Task<IList<ChartItemDto>> GetChartData(string role, string fromDate, string toDate, string userId, string SysId)
        {
            var filter = new ChartFilterSpecification(role);
            var charts = await _repository.ListAsync(filter);
            charts.ToList().ForEach(o =>
            {
                var replace = o.Command
                .Replace("#picker1", fromDate)
                .Replace("#picker2", toDate)
                .Replace("#UserId", userId)
                .Replace("#picker3", SysId==""?"0": SysId);
                o.Command = replace;
            });
            var list = await _reportRepository.ExecuteDataTableSql(charts, null);

            var result = charts.Select(o => new ChartItemDto
                {SeriesName = o.SeriesName, Title = o.Title,
                    Type = o.Type, Color = o.Color, ClassName = o.ClassName, Style = o.Style, ChartOptions = o.ChartOptions, AccessRight = o.AccessRight})
                .ToList();
            for (var i = 0; i < charts.Count; i++)
            {
                result[i].Data = list[i];
                result[i].Categories = list[i].Select(x => x.GetObjectValue<string>("name") ?? "ناشناس").ToList();
                //result[i].Data = new List<object>
                //{
                //    new
                //    {
                //        Name = list[i].Select(x => x.GetObjectValue<string>("Name") ?? "ناشناس"),
                //        y = list[i].Select(o => o.GetObjectValue<int>("Value"))
                //    }
                //};
            }
            return result;
        }
    }
}