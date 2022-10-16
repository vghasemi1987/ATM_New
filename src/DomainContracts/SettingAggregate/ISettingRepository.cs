using DomainContracts.Commons;
using DomainEntities.SettingAggregate;
using System.Threading.Tasks;

namespace DomainContracts.SettingAggregate
{
    public interface ISettingRepository : IRepository<Setting>, IAsyncRepository<Setting>
    {
        Task<Setting> FindByIdAsync(short id);
    }
}
