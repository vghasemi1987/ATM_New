using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainContracts.Commons
{
    public interface ISmsSender
    {
        Task SendSmsAsync(List<string> phone, string message);
    }
}