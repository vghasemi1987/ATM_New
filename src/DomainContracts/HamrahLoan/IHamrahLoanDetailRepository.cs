using DomainContracts.Commons;
using DomainEntities.HamrahLoan;
using KendoHelper;

namespace DomainContracts.HamrahLoan
{
    public interface IHamrahLoanDetailRepository : IRepository<HamrahLoanDetail>, IAsyncRepository<HamrahLoanDetail>
    {
        DataSourceResult GetList(DataSourceRequest request, int branchId, bool isAdmin);
        DataSourceResult GetList(DataSourceRequest request,int headerId, int branchId);
    }
}
