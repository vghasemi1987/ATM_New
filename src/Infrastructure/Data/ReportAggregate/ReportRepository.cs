using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DomainContracts.ReportAggregate;
using DomainEntities.ReportAggregate;
using Infrastructure.Data.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.ReportAggregate
{
    public class ReportBoxRepository : EfRepository<ReportBox>, IReportBoxRepository
    {
        private DbSet<ReportBox> DbSet { get; }
        public ReportBoxRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<ReportBox>();
        }

        public async Task<Dictionary<string, dynamic>> ExecuteCommand(string query, Dictionary<string, dynamic> dic)
        {
            using (var connection = DbContext.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows) return dic;
                        foreach (var i in dic.Keys.ToList())
                        {
                            while (await reader.ReadAsync())
                            {
                                dic[i] = (int)reader[i];
                            }
                            reader.NextResult();
                        }
                    }
                }
            }
            return dic;
        }

        public async Task<List<List<object>>> ExecuteDataTableSql(IEnumerable<Chart> charts, SqlParameter[] cmdParams)
        {
            var listItems = new List<List<object>>();
            using (var conn = new SqlConnection(DbContext.Database.GetDbConnection().ConnectionString))
            {
                await conn.OpenAsync();
                foreach (var item in charts)
                {
                    using (var dt = new DataTable())
                    {
                        using (var da = new SqlDataAdapter(item.Command, conn))
                        {
                            da.Fill(dt);

                            listItems.Add(dt.Rows.Cast<DataRow>()
                                .Select(row => new { name = row[0].ToString(), y = (dynamic)row[1] })
                                .ToList<object>());
                        }
                    }
                }
            }
            return listItems;
        }

        public Task<List<ReportBox>> GetByRoleReportBoxes(string roleName)
        {
            return DbSet
                .Where(o => o.AccessRight.Contains(roleName))
                .AsNoTracking()
                .ToListAsync();
        }
        public void Delete(IEnumerable<int> ids)
        {
            //var selectedRecords = await DbContext.ReportBoxes.Join(ids, r => r.Id, at => at, (r, at) => r).ToListAsync();
            foreach (var record in ids)
            {
                DbSet.Remove(new ReportBox{Id = record});
            }
        }
    }
}