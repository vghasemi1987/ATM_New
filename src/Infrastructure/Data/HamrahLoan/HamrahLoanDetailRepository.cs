using DomainContracts.HamrahLoan;
using DomainEntities.HamrahLoan;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data.HamrahLoan
{
    public class HamrahLoanDetailRepository : EfRepository<HamrahLoanDetail>, IHamrahLoanDetailRepository
    {
        private DbSet<HamrahLoanDetail> DbSet { get; }


        public HamrahLoanDetailRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<HamrahLoanDetail>();
        }

        public DataSourceResult GetList(DataSourceRequest request, int headerId, int branchId)
        {
            return DbSet
            .Include(o => o.UserChangeStatus)
            .Include(o => o.Header.Branch)
                 .Where(o => o.HeaderId.Equals(headerId) && o.Header.BranchId.Equals(branchId))
                 .Select(o => new HamrahLoanDetailDto
                 {
                     Id = o.Id,
                     CreateDate = o.CreateDate,
                     Status = o.Status,
                     StatusDate = o.StatusDate,
                     HeaderId = o.HeaderId,
                     Amount = o.Amount,
                     FolowNumber = o.FolowNumber,
                     LoanDate = o.LoanDate,
                     LoanNumber = o.LoanNumber,
                     UserChangeStatus = o.UserChangeStatus.Name,
                     StatusId= o.Status,
                     BranchTitle = o.Header.Branch.Title
                 })
                 .AsNoTracking()
                 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
        public DataSourceResult GetList(DataSourceRequest request, int branchId, bool isAdmin)
        {
            return DbSet
            .Include(o => o.UserChangeStatus)
            .Include(o => o.Header.Branch)
            .Where(o => o.Header.BranchId.Equals(branchId) || isAdmin)
                 .Select(o => new HamrahLoanDetailDto
                 {
                     Id = o.Id,
                     CreateDate = o.CreateDate,
                     Status = o.Status,
                     StatusDate = o.StatusDate,
                     HeaderId = o.HeaderId,
                     Amount = o.Amount,
                     FolowNumber = o.FolowNumber,
                     LoanDate = o.LoanDate,
                     LoanNumber = o.LoanNumber,
                     UserChangeStatus = o.UserChangeStatus.Name,
                     StatusId = o.Status,
                     BranchTitle=o.Header.Branch.Title
                 })
                 .AsNoTracking()
                 .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
    }
}
