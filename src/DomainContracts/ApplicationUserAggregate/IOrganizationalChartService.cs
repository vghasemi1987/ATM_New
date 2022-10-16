using DomainEntities.ApplicationUserAggregate;
using System.Collections.Generic;

namespace DomainContracts.ApplicationUserAggregate
{
    public interface IOrganizationalChartService
    {
        IList<OrganizationalChart> GetAll();
    }
}
