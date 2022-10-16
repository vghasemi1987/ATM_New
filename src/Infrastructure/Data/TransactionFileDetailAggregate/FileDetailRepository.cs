using ApplicationCommon;
using DomainContracts.TransactionAggregate;
using DomainEntities.AccountAggregate;
using DomainEntities.BranchAggregate;
using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.TransactionFileDetailAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TransactionFileDetailAggregate
{
    public class FileDetailRepository : EfRepository<FileDetail>, IFileDetailRepository
    {
        private DbSet<FileDetail> FileDetailDbSet { get; }
        private DbSet<Branch> BranchesDbSet { get; }
        private DbSet<Account> AccountDbSet { get; }

        public FileDetailRepository(AtmContext dbContext) : base(dbContext)
        {
            FileDetailDbSet = dbContext.Set<FileDetail>();
            BranchesDbSet = dbContext.Set<Branch>();
            AccountDbSet = DbContext.Set<Account>();
        }

        public Task<FileDetail> FindByIdAsync(int id)
        {
            return FileDetailDbSet.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Delete(IEnumerable<int> ids)
        {
            foreach (var record in ids)
            {
                FileDetailDbSet.Remove(new FileDetail { Id = record });
            }
        }

        public DataSourceResult GetList(DataSourceRequest request, int? fileId)
        {
            return FileDetailDbSet
                .Where(o => !fileId.HasValue || o.FileId.Equals(fileId))
                .Select(o => new FileDetailListDto
                {
                    Id = o.Id,
                    StatusId = o.StatusId.GetValueOrDefault(),
                    Date = o.Date,
                    Time = o.Time,
                    FileId = o.FileId,
                    CardNumber = o.CardNumber,
                    Operation = o.Operation,
                    Amount = o.Amount,
                    AtmCode = o.AtmCode,
                    IsRefahi = o.IsRefahi,
                    ResponseCode = o.ResponseCode,
                    StatusTitle = o.Status.Title,
                    TransactionNumber = o.TransactionNumber,
                    DocumentCode = o.DocumentCode,
                    UserDescription = o.UserDescription,
                    DocumentPrintedDate = o.DocumentPrintedDate,
                    ShetabiPrintedDate = o.ShetabiPrintedDate,
                })
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        public DataSourceResult GetList(DataSourceRequest request, int? branchId, int? fileId, AdvancedFilter filter, List<EnumStatus?> stateIds)
        {
            try
            {
                var result = FileDetailDbSet
                               .Include(o => o.File)
                               //.Include(o => o.Status)
                               .Include(o => o.File.Branch)
                               .Where(o =>
                               (!fileId.HasValue || o.FileId.Equals(fileId)) &&
                                           (!branchId.HasValue || o.File.BranchId.Equals(branchId)) &&
                                           (!stateIds.Any() || stateIds.Contains(o.StatusId)) &&
                                           (!filter.BranchId.HasValue || o.File.BranchId.Equals(filter.BranchId)) &&
                                           (!filter.BranchHeadId.HasValue || o.File.BranchHeadId.Equals(filter.BranchHeadId)) &&
                                           (!filter.UserId.HasValue || o.File.UserId.Equals(filter.UserId))
                                           )
                               .Select(o => new FileDetailListDto
                               {
                                   Id = o.Id,
                                   StatusId = o.StatusId,
                                   Date = o.Date,
                                   Time = o.Time,
                                   FileId = o.FileId,
                                   CardNumber = o.CardNumber,
                                   Operation = o.Operation,
                                   Amount = o.Amount,
                                   AtmCode = o.AtmCode,
                                   IsRefahi = o.IsRefahi,
                                   ResponseCode = o.ResponseCode,
                                   StatusTitle = o.Status.Title,
                                   TransactionNumber = o.TransactionNumber,
                                   DocumentCode = o.DocumentCode,
                                   UserDescription = o.UserDescription,

                                   ReferenceDateOperator = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToBranchBoss) ?
                                       o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToBranchBoss).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
                                   
                                   
                                   
                                   
                                   ReferenceDateBoss = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToAccounting) ?
                                       o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToAccounting).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",



                                   DocumentPrintedDate = o.DocumentPrintedDate,
                                   ShetabiPrintedDate = o.ShetabiPrintedDate,
                                   BranchName = o.File.Branch.Title,
                                   BranchCode = o.File.Branch.Code,
                                   FileName = o.File.Name,
                               })
                               .AsNoTracking()
                               .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
                return result;
            }
            catch (System.Exception ex)
            {

                throw;
            }

        }

        public DataSourceResult GetShetabiFileList(DataSourceRequest request, int? fromDate, int? toDate)
        {
            //var withoutDate = fromDate != null && toDate != null;
            return FileDetailDbSet
                .Include(o => o.File.User.Branch)
                .Where(o =>
                    ((!fromDate.HasValue && !toDate.HasValue) || o.IsShetabiPrinted.Equals(false)) &&
                    (o.StatusId.Equals(EnumStatus.SendToAccounting) || o.StatusId.Equals(EnumStatus.FinalRegistration)) &&
                    //(!filter.UserId.HasValue || o.File.UserId.Equals(filter.UserId))&&
                    (!fromDate.HasValue || int.Parse(o.Date) >= fromDate) &&
                    (!toDate.HasValue || int.Parse(o.Date) <= toDate) && o.IsRefahi == false)
                .Select(o => new FileDetailListDto
                {
                    Id = o.Id,
                    StatusId = o.StatusId.GetValueOrDefault(),
                    Date = o.Date,
                    Time = o.Time,
                    FileId = o.FileId,
                    CardNumber = o.CardNumber,
                    Operation = o.Operation,
                    Amount = o.Amount,
                    AtmCode = o.AtmCode,
                    IsRefahi = o.IsRefahi,
                    ResponseCode = o.ResponseCode,
                    StatusTitle = o.Status.Title,
                    TransactionNumber = o.TransactionNumber,
                    IsShetabiPrinted = o.IsShetabiPrinted,
                    //IsDocumentPrinted = o.IsDocumentPrinted,
                    DocumentCode = o.DocumentCode,
                    BranchName = o.File.User.Branch.Title,
                    //BranchId=o.File.User.BranchId.Value,
                    UserName = o.File.User.Name,
                    BranchCode = o.File.User.Branch.Code,
                    //UserId=o.File.UserId,
                    DocumentPrintedDate = o.DocumentPrintedDate,
                    ShetabiPrintedDate = o.ShetabiPrintedDate,
                })
                .AsNoTracking()
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }

        //public Task<List<FileDetailListDto>> GetFileTransaction(List<int> ids)
        //{
        //   var r= FileDetailDbSet
        //          .Include(o => o.File.User.Branch)
        //          .Where(o=> ids.Contains(o.FileId))
        //        //(o => ids.Contains(o.Id))
        //          //.Where(o => !fileId.HasValue || o.FileId.Equals(fileId))
        //          .Select(o => new FileDetailListDto
        //          {
        //              Id = o.Id,
        //              StatusId = o.StatusId.GetValueOrDefault(),
        //              Date = o.Date,
        //              Time = o.Time,
        //              FileId = o.FileId,
        //              CardNumber = o.CardNumber,
        //              Operation = o.Operation,
        //              Amount = o.Amount,
        //              AtmCode = o.AtmCode,
        //              BranchName = o.File.User.Branch.Title,
        //              BranchId=o.File.User.BranchId.Value,
        //              UserName = o.File.User.Name,
        //              BranchCode = o.File.User.Branch.Code,
        //              UserId=o.File.UserId,
        //              IsRefahi = o.IsRefahi,
        //              ResponseCode = o.ResponseCode,
        //              StatusTitle = o.Status.Title,
        //              TransactionNumber = o.TransactionNumber,
        //              DocumentCode = o.DocumentCode,
        //              DocumentPrintedDate = o.DocumentPrintedDate,
        //              UserDescription = o.UserDescription,
        //              FileName=o.File.Name,
        //              ReferenceDateOperator = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToBranchBoss) ?
        //                               o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToBranchBoss).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
        //              ReferenceDateBoss = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToAccounting) ?
        //                               o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToAccounting).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",


        //          }).ToListAsync();
        //    return r;
        //  }

            //public Task<List<FileDetailList>> GetFileTransaction(int? fileId)
            //{
            //    return DbSet
            //        .Where(o => !fileId.HasValue || o.FileId.Equals(fileId))
            //        .Include(o => o.Status)
            //        .AsNoTracking()
            //        .Select(o => new FileDetailList
            //        {
            //            Id = o.Id,
            //            StatusId = o.StatusId.GetValueOrDefault(),
            //            Date = o.Date,
            //            Time = o.Time,
            //            FileId = o.FileId,
            //            CardNumber = o.CardNumber,
            //            Operation = o.Operation,
            //            Amount = o.Amount,
            //            AtmCode = o.AtmCode,
            //            IsRefahi = o.IsRefahi,
            //            ResponseCode = o.ResponseCode,
            //            StatusTitle = o.Status.Title,
            //            TransactionNumber = o.TransactionNumber,
            //            DocumentCode = o.DocumentCode,
            //            UserDescription = o.UserDescription
            //        })
            //        .OrderByDescending(o => o.IsShetabiPrinted)
            //        .ToListAsync();
            //}

            public IEnumerable<FileDetail> GetListById(List<int> ids)
        {
            return FileDetailDbSet
                .Where(o => ids.Contains(o.Id))
                .AsNoTracking()
                .ToList();
        }

        public Task<List<FileDetail>> GetListByDocumentCode(int documentCode)
        {
            return FileDetailDbSet.Where(o => o.DocumentCode == documentCode)
                .ToListAsync();
        }

        public Task<FileDetail> GetByCardNumberAndTransactionNumber(string cardNum, string transactionNum)
        {
            return FileDetailDbSet
                .Where(o => o.CardNumber == cardNum && o.TransactionNumber == transactionNum)
                .FirstOrDefaultAsync();
        }

        public int FindMaxDocumentCode()
        {
            return FileDetailDbSet.Max(o => o.DocumentCode.GetValueOrDefault()) + 1;
        }

        public DataSourceResult GetSumBranchTransaction(DataSourceRequest request, int? fromDate, int? toDate)
        {
            //var withoutDate = ;
            return FileDetailDbSet
                .Where(o => (o.DocumentCode > 0) &&
                            ((!fromDate.HasValue && !toDate.HasValue) || o.IsDocumentPrinted.Equals(false)) &&
                            (!fromDate.HasValue || int.Parse(o.Date) >= fromDate) &&
                            (!toDate.HasValue || int.Parse(o.Date) <= toDate) &&
                            (o.StatusId == EnumStatus.SendToAccounting || o.StatusId == EnumStatus.FinalRegistration)
                            && o.IsRefahi == false
                            )
                .Join(BranchesDbSet, cr => cr.File.BranchId, bn => bn.Id, (cr, bn) => new { cr, bn })
                .Join(AccountDbSet, ac => ac.bn.Id, tr => tr.BranchId, (ac, tr) => new { ac, tr })
                .GroupBy(x => new
                {
                    x.ac.cr.DocumentCode,
                    x.ac.cr.File.BranchId,
                    x.ac.bn.Title,
                    x.tr.AccountNo,
                    x.ac.cr.IsDocumentPrinted,
                    x.ac.bn.Code,
                    x.ac.cr.Date,
                    x.ac.cr.TransactionNumber
                })
                .Select(g => new DocumentationDto
                {
                    TransactionDate = g.Key.Date,
                    Id = g.Key.DocumentCode.GetValueOrDefault(),
                    Amount = g.Sum(i => i.ac.cr.Amount.GetValueOrDefault()),
                    BranchName = g.Key.Title,
                    AccountNo = g.Key.AccountNo.GetValueOrDefault(),
                    IsDocumentPrinted = g.Key.IsDocumentPrinted,
                    BranchCode = g.Key.Code,
                    TransactionNumber = g.Key.TransactionNumber
                })
                .ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
        }


        public Task<List<DocumentationDto>> GetTransactionByDocumentCode(List<int> ids)
        {
            return FileDetailDbSet
                .Where(o => ids.Contains(o.DocumentCode.GetValueOrDefault()) && o.IsRefahi == false &&
                (o.StatusId == EnumStatus.SendToAccounting || o.StatusId == EnumStatus.FinalRegistration))
                .Join(BranchesDbSet, cr => cr.File.BranchId, bn => bn.Id, (cr, bn) => new { cr, bn })
                .Join(AccountDbSet, ac => ac.bn.Id, tr => tr.BranchId, (ac, tr) => new { ac, tr })
                .GroupBy(x => new
                {
                    x.ac.cr.DocumentCode,
                    x.ac.cr.File.BranchId,
                    x.ac.bn.Title,
                    x.tr.AccountNo,
                    x.ac.cr.IsDocumentPrinted,
                    x.ac.bn.Code,
                    x.ac.cr.Date,
                    x.ac.cr.TransactionNumber
                })
                .Select(g => new DocumentationDto
                {
                    TransactionDate = g.Key.Date,
                    Id = g.Key.DocumentCode.GetValueOrDefault(),
                    Amount = g.Sum(i => i.ac.cr.Amount.GetValueOrDefault()),
                    BranchName = g.Key.Title,
                    AccountNo = g.Key.AccountNo.GetValueOrDefault(),
                    IsDocumentPrinted = g.Key.IsDocumentPrinted,
                    BranchCode = g.Key.Code,
                    TransactionNumber = g.Key.TransactionNumber
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}