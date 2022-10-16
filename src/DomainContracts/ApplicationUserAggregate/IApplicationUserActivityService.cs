using System.Threading.Tasks;
using DomainEntities.ApplicationUserAggregate;

namespace DomainContracts.ApplicationUserAggregate
{
    public interface IApplicationUserActivityService
    {
        Task Save(ApplicationUserActivity model);
    }
}