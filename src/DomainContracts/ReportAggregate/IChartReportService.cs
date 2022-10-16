using System.Collections.Generic;
using System.Threading.Tasks;
using DomainEntities.ReportAggregate;

namespace DomainContracts.ReportAggregate
{
    public interface IChartReportService
    {
        Task<IList<ChartItemDto>> GetChartData(string role, string fromDate, string toDate, string userId,string SysId);
    }

    
}