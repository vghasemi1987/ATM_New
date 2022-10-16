using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainContracts.NotificationAggregate;
using DomainEntities.NotificationAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.NotificationAggregate
{
    public class NotificationRepository : EfRepository<NotificationItem>, INotificationRepository
    {
        private DbSet<NotificationItem> DbSet { get; }

        public NotificationRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<NotificationItem>();
        }

        public Task<List<NotificationItem>> GetByUserIdNotificationsAsync(int userId, int count)
        {
            return DbSet.Where(o => o.ForUserId == userId)
                .OrderByDescending(o => o.Id)
                .Take(count)
                .Include(o => o. CreatedByUser)
                .AsNoTracking()
                .ToListAsync();
        }

        public DataSourceResult GetByUserIdNotificationsAsync(DataSourceRequest request, int userId)
        {
            return DbSet.Where(o => o.ForUserId == userId)
                .OrderByDescending(o => o.Id)
                .Include(o => o.CreatedByUser)
                .Include(o=>o.Category)
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
    }
}