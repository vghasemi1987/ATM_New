using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.TransactionFileDetailAggregate;

namespace DomainContracts.TransactionAggregate
{
    public interface ITypeRepository : IRepository<Type>, IAsyncRepository<Type>
    {
        Task<List<Type>> GetList();
    }
}