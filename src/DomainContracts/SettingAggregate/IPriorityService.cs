using System.Collections.Generic;
using System.Threading.Tasks;
using DomainEntities.SettingAggregate;

namespace DomainContracts.SettingAggregate
{
    public interface IPriorityService
    {
        Task<IList<Priority>> GetAllAsync();
    }
}
