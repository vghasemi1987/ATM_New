using DomainContracts.Commons;
using DomainEntities.ApplicationUserAggregate;

namespace DomainContracts.ApplicationUserAggregate
{
    public interface IApplicationUserActivityRepository : IRepository<ApplicationUserActivity>, IAsyncRepository<ApplicationUserActivity>
    {

    }
}