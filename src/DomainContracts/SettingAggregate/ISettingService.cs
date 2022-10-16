using System.Threading.Tasks;
using DomainEntities.SettingAggregate;

namespace DomainContracts.SettingAggregate
{
    public interface ISettingService
    {
        Task SaveEmailAsync(Setting model);
        Task SaveSmsAsync(Setting model);
        Task SaveAppAsync(Setting model);
    }
}