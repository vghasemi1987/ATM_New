using DomainContracts.HamrahLoan;
using DomainEntities.HamrahLoan;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data.HamrahLoan
{
    public class HamrahLoanHeaderRepository : EfRepository<HamrahLoanHeader>, IHamrahLoanHeaderRepository
    {
        private DbSet<HamrahLoanHeader> DbSet { get; }


        public HamrahLoanHeaderRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<HamrahLoanHeader>();
        }

        public DataSourceResult GetList(DataSourceRequest request, int branchId)
        {

            return DbSet
                .Include(o => o.Branch)
                .Include(o => o.UserCreate)
                .Where(o => o.BranchId.Equals(branchId))
                .Select(o => new HamrahLoanHeaderDto
                {
                    Id = o.Id,
                    BranchCode = o.Branch.Code,
                    BranchTitle = o.Branch.Title,
                    Title = o.Title,
                    CreateDate = o.CreateDate,
                    DetailsCount = o.Details.Count(),
                    Status = o.Status,
                    StatusDate = o.StatusDate,
                    UserCreate = o.UserCreate.Name,
                })
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
        /*private string GetStatusTitle(HamrahLoanStatus hamrahLoanStatus)
        {
            switch (hamrahLoanStatus)
            {

                case HamrahLoanStatus.Pending:
                    return "درحال بررسی";
                case HamrahLoanStatus.Reviewed:
                    return "بررسی شده";
                case HamrahLoanStatus.NoDiscrepancy:
                    return "عدم مغایرت";
                case HamrahLoanStatus.NoSama:
                    return "شناسه در سما یافت نشد";
                case HamrahLoanStatus.NoHamrah:
                    return "شناسه در همراه یافت نشد";
                case HamrahLoanStatus.DiscrepancyByUser:
                    return "مغایرت دارد توسط کاربر";
                case HamrahLoanStatus.NoDiscrepancyByUser:
                    return "عدم مغایرت توسط کاربر";
                default:
                    return "";

            }
        }*/
    }
}
