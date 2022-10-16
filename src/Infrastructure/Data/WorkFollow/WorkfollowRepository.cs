using System;
using System.Collections.Generic;
using System.Linq;
using DomainContracts.TransactionAggregate;
using DomainEntities.ApplicationUserAggregate;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.WorkfollowAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.WorkFollow
{
    public class WorkfollowRepository : EfRepository<Workfollow>, IWorkfollowRepository
    {
        private DbSet<Workfollow> DbSet { get; }
        public WorkfollowRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<Workfollow>();
        }

        public DataSourceResult GetTransactionWorkFollow(DataSourceRequest request)
        {
           return DbSet
                .Join(DbContext.Set<ApplicationUser>(), cr => cr.UserId, bn => bn.Id, (cr, bn) => new { cr, bn })
                .Select(o => new WorkfollowDetailDto
                {
                    FileDetailId = o.cr.FileDetailId,
                    Id = o.cr.Id,
                    Status = o.cr.Status.Title,
                    UpdateDateTime = o.cr.UpdateDateTime,//.ToPersianDateTime("yyyy/MM/dd"),
                    UserName = o.bn.Name
                })
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
        public DateTime GetUpdateDateTimeWorkfollows(EnumStatus enumStatus, int fileDetailId)
        {
            return DbSet
                    .OrderByDescending(o => o.UpdateDateTime)
                    .SingleOrDefault(o => o.StatusId == enumStatus).UpdateDateTime;
        }

    }
}