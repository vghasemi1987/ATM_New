using ApplicationCommon;
using DomainContracts.ATMUnknownTransactionsAggregate;
using DomainEntities.ATMUnknownTransactionsAggregate;
using DomainEntities.TransactionFileAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.ATMUnknownTransactionsAggregate
{
    public class ATMUnknownTransactionsRepository : EfRepository<ATMUnknownTransactions>, IATMUnknownTransactionsRepository
    {
        private DbSet<ATMUnknownTransactions> DbSet { get; }
        private DbSet<WorkfollowATMUnknownTransactions> DbSetWorkfollow { get; }


        public ATMUnknownTransactionsRepository(AtmContext dbContext) : base(dbContext)
        {
            DbSet = dbContext.Set<ATMUnknownTransactions>();
            DbSetWorkfollow = dbContext.Set<WorkfollowATMUnknownTransactions>();

        }
        public DataSourceResult GetList(DataSourceRequest request, string branchCode, List<EnumStatus?> stateIds)
        {
            return DbSet
               .Where(o =>
                           o.BranchCode.Equals(branchCode)
                           &&
                           (!stateIds.Any() || stateIds.Contains(o.StatusWorkfollowId)  )
                           )
               // (!filter.BranchId.HasValue || o.File.BranchId.Equals(filter.BranchId)) &&
               //(!filter.BranchHeadId.HasValue || o.File.BranchHeadId.Equals(filter.BranchHeadId)) &&
               //(!filter.UserId.HasValue || o.File.UserId.Equals(filter.UserId)))
               .Select(o => new ATMUnknownTransactionsDto
               {
                   Id = o.Id,
                   StatusWorkfollowId = o.StatusWorkfollowId.GetValueOrDefault(),
                   StatusWorkfollowTitle = o.StatusWorkfollow.Title,
                   BranchCode = o.BranchCode,
                   ATMUnknownTransactionsID = o.ATMUnknownTransactionsID,
                   ATM = o.ATM,
                   Branch = o.Branch,
                   ATMID = o.ATMID,
                   CardNumber = o.CardNumber,
                   CardType = o.CardType,
                   DeterminationDate = o.DeterminationDate.ToPersianDateTime("yyyy/MM/dd"),
                   Status = o.Status,
                   StatusID = o.StatusID,
                   TransactionAmount = o.TransactionAmount,
                   TransactionDate = o.TransactionDate.ToPersianDateTime("yyyy/MM/dd"),
                   TransactionNumber = o.TransactionNumber,
                   TransactionTime = o.TransactionTime,
                   UserDescription = o.UserDescription,
                   ATMAtletCode = o.ATMAtletCode
               })
               .AsNoTracking()
               .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }
        public IEnumerable<ATMUnknownTransactions> GetListById(List<int> ids)
        {
            return DbSet
                .Where(o => ids.Contains(o.Id))
                .AsNoTracking()
                .ToList();
        }

        public DataSourceResult GetList(DataSourceRequest request, List<string> branchCodes, bool isAdmin)
        {
            return DbSet
               .Where(o =>isAdmin || branchCodes.Any(bc => bc.Equals(o.BranchCode)))
               .Select(o => new ATMUnknownTransactionsDto
               {
                   Id = o.Id,
                   StatusWorkfollowId = o.StatusWorkfollowId.GetValueOrDefault(),
                   StatusWorkfollowTitle = o.StatusWorkfollow.Title,
                   BranchCode = o.BranchCode,
                   ATMUnknownTransactionsID = o.ATMUnknownTransactionsID,
                   ATM = o.ATM,
                   Branch = o.Branch,
                   ATMID = o.ATMID,
                   CardNumber = o.CardNumber,
                   CardType = o.CardType,
                   DeterminationDate = o.DeterminationDate.ToPersianDateTime("yyyy/MM/dd"),
                   Status = o.Status,
                   StatusID = o.StatusID,
                   TransactionAmount = o.TransactionAmount,
                   TransactionDate = o.TransactionDate.ToPersianDateTime("yyyy/MM/dd"),
                   TransactionNumber = o.TransactionNumber,
                   TransactionTime = o.TransactionTime,
                   UserDescription = o.UserDescription,
                   ATMAtletCode = o.ATMAtletCode
               })
               .AsNoTracking()
               .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        public void AddWorkfollows(List<WorkfollowATMUnknownTransactions> workfollows)
        {
            DbSetWorkfollow.AddRange(workfollows);
        }

        public List<WorkfollowATMUnknownTransactionsDto> GetListWorkfollows(int aTMUnknownId)
        {
            return DbSetWorkfollow
                .Include(o => o.User)
                .Where(o => o.ATMUnknownTransactionsId == aTMUnknownId)
                .Select(o => new WorkfollowATMUnknownTransactionsDto
                    {
                        CreateDateTime = o.CreateDateTime.ToPersianDateTime("yyyy/MM/dd"),
                        Id=o.Id,
                        Status=o.Status,
                        User=o.User.Name,
                        UserMessage=o.UserMessage
                    })
                .ToList();
        }
    }
}
