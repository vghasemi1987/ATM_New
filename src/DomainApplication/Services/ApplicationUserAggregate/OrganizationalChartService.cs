using DomainContracts.ApplicationUserAggregate;
using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;
using System.Collections.Generic;

namespace DomainApplication.Services.ApplicationUserAggregate
{
    public class OrganizationalChartService : IOrganizationalChartService
    {
        private readonly IRepository<OrganizationalChart> _repository;

        public OrganizationalChartService(IRepository<OrganizationalChart> repository)
        {
            _repository = repository;
        }
        public IList<OrganizationalChart> GetAll()
        {
            return _repository.ListAll();
        }
        public OrganizationalChart FindByIdAsync(short id)
        {
            return _repository.GetSingleBySpec(o => o.Id.Equals(id));
        }
    }
}
