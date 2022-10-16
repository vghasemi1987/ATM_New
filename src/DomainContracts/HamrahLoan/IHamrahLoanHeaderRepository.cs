using DomainContracts.Commons;
using DomainEntities.HamrahLoan;
using KendoHelper;

namespace DomainContracts.HamrahLoan
{
    public interface IHamrahLoanHeaderRepository : IRepository<HamrahLoanHeader>, IAsyncRepository<HamrahLoanHeader>
    {
        DataSourceResult GetList(DataSourceRequest request, int branchId);
    }
}
