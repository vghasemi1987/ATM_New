using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.TransactionFileAggregate;
using KendoHelper;

namespace DomainContracts.TransactionAggregate
{
    public interface IFileRepository : IRepository<File>, IAsyncRepository<File>
    {
        Task<File> FindByIdAsync(int id);
        void Delete(IEnumerable<int> ids);
        DataSourceResult GetList(DataSourceRequest request, int? userId, int? branchId, DateTime? fromDate, DateTime? toDate);
        //Task<List<File>> GetListById(int id);
        Task<List<TransactionFileDto>> GetFileTransaction(List<int> ids);
        /// <summary>
        /// قرار داد موجود جهت خروجی اکسل
        /// </summary>
        /// <returns></returns>
        Task<System.IO.MemoryStream> GenerateExcelReport();
    }
}
