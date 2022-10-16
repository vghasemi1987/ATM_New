using System;
using System.Threading.Tasks;

namespace DomainContracts.Commons
{
    public interface IUnitOfWork: IDisposable
    {
        Task SaveAsync();
        void Save();
    }
}