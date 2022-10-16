using DomainContracts.Commons;
using DomainEntities.ATMUnknownTransactionsAggregate;
using DomainEntities.TransactionFileAggregate;
using KendoHelper;
using System.Collections.Generic;

namespace DomainContracts.ATMUnknownTransactionsAggregate
{
    public interface IATMUnknownTransactionsRepository : IRepository<ATMUnknownTransactions>, IAsyncRepository<ATMUnknownTransactions>
    {
        DataSourceResult GetList(DataSourceRequest request, string branchCode, List<EnumStatus?> stateIds);
        DataSourceResult GetList(DataSourceRequest request, List<string> branchCode, bool isAdmin);
        IEnumerable<ATMUnknownTransactions> GetListById(List<int> ids);
        void AddWorkfollows(List<WorkfollowATMUnknownTransactions> workfollows);
        List<WorkfollowATMUnknownTransactionsDto> GetListWorkfollows(int aTMUnknownId);
    }
}
