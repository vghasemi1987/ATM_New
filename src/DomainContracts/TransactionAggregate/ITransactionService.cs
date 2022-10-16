using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DomainEntities.TransactionFileAggregate;
using File = DomainEntities.TransactionFileAggregate.File;

namespace DomainContracts.TransactionAggregate
{
    public interface ITransactionService
    {
        Task<IList<File>> GetAllAsync();
        File FindByIdAsync(int id);
        Task<bool> CheckTransactionExist(string cardNumber, string transactionNumber);
        Task Save(File file);
        string SendToStatus(List<int> transactionIds, EnumStatus status, string messageText, int userId);
        Task<byte[]> GetShetabiFile(List<int> data);
        Task<MemoryStream> GenerateExcelReport(List<int> model);
        Task<MemoryStream> GenerateFileDetailExcelReport(List<int>  ids);
    }
}
