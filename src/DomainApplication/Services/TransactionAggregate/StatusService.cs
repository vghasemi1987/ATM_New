using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.TransactionAggregate;
using DomainEntities.TransactionFileAggregate;

namespace DomainApplication.Services.TransactionAggregate
{
    public class StatusService : IStatusService
    {
        private readonly IAsyncRepository<Status> _repository;

        public StatusService(IAsyncRepository<Status> repository)
        {
            _repository = repository;
        }

        public Task<IList<Status>> GetAllAsync()
        {
            return _repository.ListAllAsync();
        }
    }
}
