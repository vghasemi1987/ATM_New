using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using KendoHelper;

namespace DomainContracts.TransactionAggregate
{
    public interface IFileDetailRepository : IRepository<FileDetail>, IAsyncRepository<FileDetail>
    {
        Task<FileDetail> FindByIdAsync(int id);
        void Delete(IEnumerable<int> ids);
        //Task<List<FileDetailList>> GetListByFileIdWithBranchId(DataSourceRequest request, int fileId, int? branchId);
        DataSourceResult GetList(DataSourceRequest request, int? branchId, int? fileId, AdvancedFilter filter, List<EnumStatus?> statusIds);
        DataSourceResult GetList(DataSourceRequest request, int? fileId);
        DataSourceResult GetShetabiFileList(DataSourceRequest request, int? fromDate, int? toDate);
        //Task<List<FileDetailListDto>> GetFileTransaction(List<int> ids);
        Task<List<FileDetail>> GetListByDocumentCode(int documentCode);
        Task<FileDetail> GetByCardNumberAndTransactionNumber(string cardNum, string transactionNum);
        IEnumerable<FileDetail> GetListById(List<int> ids);
        DataSourceResult GetSumBranchTransaction(DataSourceRequest request, int? fromDate, int? toDate);
        int FindMaxDocumentCode();
        Task<List<DocumentationDto>> GetTransactionByDocumentCode(List<int> ids);
    }
}
