using DomainContracts.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.WorkfollowAggregate;
using KendoHelper;
using System;

namespace DomainContracts.TransactionAggregate
{
    public interface IWorkfollowRepository : IRepository<Workfollow>, IAsyncRepository<Workfollow>
    {
        DataSourceResult GetTransactionWorkFollow(DataSourceRequest request);
        DateTime GetUpdateDateTimeWorkfollows(EnumStatus enumStatus, int fileDetailId);
    }
}
