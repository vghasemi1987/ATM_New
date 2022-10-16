using System.Collections.Generic;
using System.Threading.Tasks;
using DomainEntities.TransactionFileAggregate;

namespace DomainContracts.TransactionAggregate
{
    public interface IStatusService
    {
        Task<IList<Status>> GetAllAsync();
    }
}
