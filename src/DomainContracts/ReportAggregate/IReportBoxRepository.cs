using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.ReportAggregate;

namespace DomainContracts.ReportAggregate
{
    public interface IReportBoxRepository : IRepository<ReportBox>, IAsyncRepository<ReportBox>
    {
        Task<Dictionary<string, dynamic>> ExecuteCommand(string query, Dictionary<string, dynamic> dic);
        void Delete(IEnumerable<int> ids);
        Task<List<List<object>>> ExecuteDataTableSql(IEnumerable<Chart> charts, SqlParameter[] cmdParams);
        Task<List<ReportBox>> GetByRoleReportBoxes(string roleName);
    }
}