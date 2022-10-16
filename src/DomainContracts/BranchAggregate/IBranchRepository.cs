using System.Collections.Generic;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainEntities.BranchAggregate;
using KendoHelper;

namespace DomainContracts.BranchAggregate
{
    public interface IBranchRepository : IRepository<Branch>, IAsyncRepository<Branch>
    {
        DataSourceResult GetList(DataSourceRequest request);
        List<Branch> GetParentDepartments();
        bool CheckBranchExist(int code, int? id);
        List<BranchDto> GetBranches();
        List<BranchDto> GetChildDepartmentsByDepartmentId(short? departmentId);
        IEnumerable<Branch> GetAllBranches(string type);
        Task<List<BranchDto>> GetBranchList(int branchHead);
        string GetBranchCode(int id);
        List<string> GetBranchCodesByHeadId(int headId);
        IEnumerable<BranchDto> GetAllBranchHeadAndBranchSort();
        int? GetBranchHeadIdById(int Id);
    }
}
