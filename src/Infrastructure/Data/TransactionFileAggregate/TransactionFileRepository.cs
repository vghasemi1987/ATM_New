using ApplicationCommon;
using DomainContracts.TransactionAggregate;
using DomainEntities.TransactionFileAggregate;
using Infrastructure.Data.Commons;
using KendoHelper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.TransactionFileAggregate
{
	public class TransactionFileRepository : EfRepository<File>, IFileRepository
	{
		private DbSet<File> DbSet { get; }
		/// <summary>
		/// دی بی ست
		/// برای گرفتن خروجی اکسل
		/// این فیلد با استفاده از ارث بری به 
		/// دی بی ست
		/// اصلی مرتبط می شود
		/// </summary>
		private DbSet<TransactionToExelAll> DbSetTransactionToExelAll { get; }
		public TransactionFileRepository(AtmContext dbContext) : base(dbContext)
		{
			DbSet = dbContext.Set<File>();
			DbSetTransactionToExelAll = dbContext.Set<TransactionToExelAll>();
		}
		public Task<File> FindByIdAsync(int id)
		{
			return DbSet.Where(o => o.Id == id)
				.FirstOrDefaultAsync();
		}

		public void Delete(IEnumerable<int> ids)
		{
			foreach (var record in ids)
			{
				DbSet.Remove(new File { Id = record });
			}
		}

		public DataSourceResult GetList(DataSourceRequest request, int? userId, int? branchId, DateTime? fromDate, DateTime? toDate)
		{
			try
			{
				var l = DbSet
	.Include(o => o.Status)
	.Where(o =>
		(!userId.HasValue || o.UserId.Equals(userId)) &&
		(!branchId.HasValue || o.BranchId.Equals(branchId)));
				if (fromDate != null)
				{
					l = l.Where(o => o.RegDateTime >= fromDate);
				}
				if (toDate != null)
				{
					l = l.Where(o => o.RegDateTime <= toDate);
				}
				return l.Select(o => new FileListDto
				{
					Id = o.Id,
					Name = o.Name,
					RegDateTime = o.RegDateTime,
					StatusId = o.StatusId,
					BranchId = o.BranchId,
					BranchHeadId = o.BranchHeadId,
					UserName = o.User.Name,
					BranchName = o.Branch.Title,
					BranchCode = o.Branch.Code,
					LastStatusTitle = o.Status.Title,
				})
					.AsNoTracking()
					.ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter);
			}
			catch (Exception ex)
			{

				throw;
			}

		}

		public Task<List<File>> GetListById(int id)
		{
			return DbSet.Where(o => o.Id == id)
				.ToListAsync();
		}

		public async Task<List<TransactionFileDto>> GetFileTransaction(List<int> ids)
		{
			var transaction = new List<TransactionFileDto>();
			var c = await DbSet
				.Where(o => ids.Contains(o.Id)).ToListAsync();


			var b = await DbSet
				.SelectMany(x => x.FileDetails.DefaultIfEmpty(), (files, filedetails) => new { files, filedetails })
				//.SelectMany(x => x.FileDetails.DefaultIfEmpty()
				.Where(o => ids.Contains(o.files.Id))
				.ToListAsync();
			foreach (var item in b)
			{
				var transactionFile = transaction;

				if (item.filedetails != null)
				{
					transactionFile = b.Select(o => new TransactionFileDto
					{
						Date = o.filedetails.Date,
						Id = o.files.Id,
					}).ToList();
					//return transactionFile;
				}
				transactionFile = b.Select(o => new TransactionFileDto
				{
					Date = "",
					Id = o.files.Id,

				}).ToList();
				//return transactionFile;


			}
			return transaction;
			//if (b.Where(o=>o.filedetails==null)) { }


			//var r = await DbSet
			//    .SelectMany(x => x.FileDetails.DefaultIfEmpty(), (files, filedetails) => new { files, filedetails })
			//    //.SelectMany(x => x.FileDetails.DefaultIfEmpty()
			//    .Where(o => ids.Contains(o.files.Id))
			//    .Select(p => new TransactionFileDto
			//    {
			//        Date = p.filedetails.Date.Any() ? p.filedetails.Date : "",
			//        Id = p.files.Id,
			//        StatusId = p.files.StatusId.GetValueOrDefault(),
			//        Time = p.filedetails.Time,
			//        //FileId = o.FileId,
			//        CardNumber = p.filedetails.CardNumber,
			//        Operation = p.filedetails.Operation,
			//        Amount = p.filedetails.Amount,
			//        AtmCode = p.filedetails.AtmCode,
			//        BranchName = p.files.Branch.Title,//o.File.User.Branch.Title,
			//        BranchId = p.files.BranchId,//o.File.User.BranchId.Value,
			//        UserName = p.files.User.Name, //o.File.User.Name,
			//        BranchCode = p.files.Branch.Code,//o.file.user.branch.code,
			//        UserId = p.files.UserId,
			//        IsRefahi = p.filedetails.IsRefahi,
			//        ResponseCode = p.filedetails.ResponseCode,
			//        StatusTitle = p.files.Status.Title,
			//        TransactionNumber = p.filedetails.TransactionNumber,
			//        DocumentCode = p.filedetails.DocumentCode,
			//        DocumentPrintedDate = p.filedetails.DocumentPrintedDate,
			//        UserDescription = p.filedetails.UserDescription,
			//        Name = p.files.Name,
			//        ReferenceDateOperator = p.filedetails.Workfollows.Any(n => n.StatusId == EnumStatus.SendToBranchBoss) ?
			//                                           p.filedetails.Workfollows.Where(n => n.StatusId == EnumStatus.SendToBranchBoss).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//        ReferenceDateBoss = p.filedetails.Workfollows.Any(n => n.StatusId == EnumStatus.SendToAccounting) ?
			//                                           p.filedetails.Workfollows.Where(n => n.StatusId == EnumStatus.SendToAccounting).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//        IsDocumentPrinted = p.filedetails.IsDocumentPrinted,
			//        BranchHeadId = p.files.BranchHeadId
			//    }).ToListAsync();
			//return r;




			//.Where(o => ids.Contains(o.Id))
			//.Select(o => new TransactionFileDto
			//{
			//    CardNumber = o.FileDetails.Any(o=>o.CardNumber),
			//      Operation = o.filedetails.Operation,
			//    BranchName = o.Branch.Title,
			//    BranchId = o.BranchId,//o.File.User.BranchId.Value,
			//    UserName = o.User.Name, //o.File.User.Name,
			//    BranchCode = o.Branch.Code,//o.file.user.branch.code,
			//    UserId = o.UserId,
			//}).ToListAsync();



			//var r = DbSet
			//     .SelectMany(x => x.FileDetails, (files, filedetails) => new { files, filedetails })

			//     //.Include(o => o.FileDetails).Include(o => o.Branch)
			//     .Where(o => ids.Contains(o.files.Id))
			//   .Select(o => new FileListDto
			//   {
			//       Date = o.filedetails.Date,
			//       Id = o.files.Id,
			//       StatusId = o.files.StatusId.GetValueOrDefault(),
			//       Time = o.filedetails.Time,
			//       //FileId = o.FileId,
			//       CardNumber = o.filedetails.CardNumber,
			//       Operation = o.filedetails.Operation,
			//       Amount = o.filedetails.Amount,
			//       AtmCode = o.filedetails.AtmCode,
			//       BranchName = o.files.Branch.Title,//o.File.User.Branch.Title,
			//       BranchId = o.files.BranchId,//o.File.User.BranchId.Value,
			//       UserName = o.files.User.Name, //o.File.User.Name,
			//       BranchCode = o.files.Branch.Code,//o.file.user.branch.code,
			//       UserId = o.files.UserId,
			//       IsRefahi = o.filedetails.IsRefahi,
			//       ResponseCode = o.filedetails.ResponseCode,
			//       StatusTitle = o.files.Status.Title,
			//       TransactionNumber = o.filedetails.TransactionNumber,
			//       DocumentCode = o.filedetails.DocumentCode,
			//       DocumentPrintedDate = o.filedetails.DocumentPrintedDate,
			//       UserDescription = o.filedetails.UserDescription,
			//       Name = o.files.Name,
			//       ReferenceDateOperator = o.filedetails.Workfollows.Any(n => n.StatusId == EnumStatus.SendToBranchBoss) ?
			//                                   o.filedetails.Workfollows.Where(n => n.StatusId == EnumStatus.SendToBranchBoss).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//       ReferenceDateBoss = o.filedetails.Workfollows.Any(n => n.StatusId == EnumStatus.SendToAccounting) ?
			//                                   o.filedetails.Workfollows.Where(n => n.StatusId == EnumStatus.SendToAccounting).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//       IsDocumentPrinted=o.filedetails.IsDocumentPrinted,
			//       BranchHeadId=o.files.BranchHeadId

			//   }).ToListAsync();
			//return r;



			//var r = FileDetailDbSet
			//       .Include(o => o.File.User.Branch)
			//       .Where(o => ids.Contains(o.FileId))
			//       (o => ids.Contains(o.Id))
			//       .Where(o => !fileId.HasValue || o.FileId.Equals(fileId))
			//       .Select(o => new FileDetailListDto
			//       {
			//           Id = o.Id,
			//           StatusId = o.StatusId.GetValueOrDefault(),
			//           Date = o.Date,
			//           Time = o.Time,
			//           FileId = o.FileId,
			//           CardNumber = o.CardNumber,
			//           Operation = o.Operation,
			//           Amount = o.Amount,
			//           AtmCode = o.AtmCode,
			//           BranchName = o.File.User.Branch.Title,
			//           BranchId = o.File.User.BranchId.Value,
			//           UserName = o.File.User.Name,
			//           BranchCode = o.File.User.Branch.Code,
			//           UserId = o.File.UserId,
			//           IsRefahi = o.IsRefahi,
			//           ResponseCode = o.ResponseCode,
			//           StatusTitle = o.Status.Title,
			//           TransactionNumber = o.TransactionNumber,
			//           DocumentCode = o.DocumentCode,
			//           DocumentPrintedDate = o.DocumentPrintedDate,
			//           UserDescription = o.UserDescription,
			//           FileName = o.File.Name,
			//           ReferenceDateOperator = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToBranchBoss) ?
			//                            o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToBranchBoss).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",
			//           ReferenceDateBoss = o.Workfollows.Any(n => n.StatusId == EnumStatus.SendToAccounting) ?
			//                            o.Workfollows.Where(n => n.StatusId == EnumStatus.SendToAccounting).Max(n => n.UpdateDateTime).ToPersianDateTime("yyyy/MM/dd HH:mm") : "",


			//       }).ToListAsync();
			//return r;
		}
		/// <summary>
		/// برای ساختن استریم جهت خروجی اکسل
		/// </summary>
		/// <returns></returns>
		public async Task<System.IO.MemoryStream> GenerateExcelReport()
		{

			System.IO.MemoryStream content = new System.IO.MemoryStream();

			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using var package = new ExcelPackage(content);

			var worksheet = package.Workbook.Worksheets.Add("DataSheet");

			worksheet.View.RightToLeft = true;
			worksheet.Cells["A:Z"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

			worksheet.Cells["A1:H1"].Merge = true;
			worksheet.Cells["A1:H1"].Value = "اطلاعات تراکنش";
			System.Drawing.Color colFromHex01 = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
			worksheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(colFromHex01);


			worksheet.Cells["I1:U1"].Merge = true;
			worksheet.Cells["I1:U1"].Value = "اطلاعات شعبه عامل و ارسال فایل";
			System.Drawing.Color colFromHex02 = System.Drawing.ColorTranslator.FromHtml("#82BDD9");
			worksheet.Cells["I1:U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["I1:U1"].Style.Fill.BackgroundColor.SetColor(colFromHex02);

			worksheet.Cells["V1:W1"].Merge = true;
			worksheet.Cells["V1:W1"].Value = "اقدام انجام شده";
			System.Drawing.Color colFromHex03 = System.Drawing.ColorTranslator.FromHtml("#5CC454");
			worksheet.Cells["V1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["V1:W1"].Style.Fill.BackgroundColor.SetColor(colFromHex03);

			System.Drawing.Color colFromHex04 = System.Drawing.ColorTranslator.FromHtml("#B9B9ED");
			worksheet.Cells["A2:W2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells["A2:W2"].Style.Fill.BackgroundColor.SetColor(colFromHex04);

			worksheet.Cells["A1:W1"].Style.Font.Name = "Tahoma";
			worksheet.Cells["A1:W1"].Style.Font.Bold = true;
			worksheet.Cells["A2:W2"].Style.Font.Name = "Tahoma";
			worksheet.View.FreezePanes(3, 1);

			worksheet.Cells[2, 1].Value = "ردیف";
			worksheet.Cells[2, 2].Value = "شماره تراکنش ";
			worksheet.Cells[2, 3].Value = "تاریخ تراکنش ";
			worksheet.Cells[2, 4].Value = "ساعت تراکنش";
			worksheet.Cells[2, 5].Value = "کد دستگاه";
			worksheet.Cells[2, 6].Value = "نوع کارت";
			worksheet.Cells[2, 7].Value = "شماره کارت";
			worksheet.Cells[2, 8].Value = "مبلغ";

			worksheet.Cells[2, 11].Value = "عنوان امور شعب";
			worksheet.Cells[2, 12].Value = "کد امور شعب";
			worksheet.Cells[2, 13].Value = "نام شعبه";

			worksheet.Cells[2, 14].Value = "کد شعبه";


			worksheet.Cells[2, 15].Value = "کد کاربر";
			worksheet.Cells[2, 16].Value = "نام کاربر";

			worksheet.Cells[2, 17].Value = "تاریخ ارجاع متصدی";
			worksheet.Cells[2, 18].Value = "تاریخ ارجاع رئیس شعبه";

			worksheet.Cells[2, 19].Value = "شماره سند";
			worksheet.Cells[2, 20].Value = "نام فایل";

			worksheet.Cells[2, 21].Value = "توضیحات";
			//hob ->21
			worksheet.Cells[2, 22].Value = "وضیعت";
			worksheet.Cells[2, 23].Value = "تاریخ برداشت فایل";




			int countExport = 1000;
			var countData = DbSetTransactionToExelAll.Count();
			//تعداد کل صفحات
			var totalPages = (int)Math.Ceiling(countData / (decimal)countExport);

			int row = 3;
			for (int index = 1; index <= totalPages; index++)
			{
				foreach (TransactionToExelAll s in DbSetTransactionToExelAll.Skip((index - 1) * countExport).Take(countExport).ToList())
				{
					if (s.CardNumber.StartsWith("589463")) continue;
					worksheet.Cells[row, 1].Value = s.Id;
					worksheet.Cells[row, 2].Value = s.TransactionNumber;
					worksheet.Cells[row, 3].Value = s.Date;
					worksheet.Cells[row, 4].Value = s.Time;
					worksheet.Cells[row, 5].Value = s.AtmCode;

					worksheet.Cells[row, 6].Value = s.CardNumber.ToCardType();
					worksheet.Cells[row, 7].Value = s.CardNumber;

					worksheet.Cells[row, 8].Value = s.Amount;
					worksheet.Cells[row, 9].Value = "";//transactionToExelAll[i].ATM;
													   //Hob 10
					worksheet.Cells[row, 11].Value = s.MainTitleBranch;
					worksheet.Cells[row, 12].Value = s.MainCodeBranch;
					worksheet.Cells[row, 13].Value = s.TitleBranch;

					//13 --> BranchCode
					worksheet.Cells[row, 14].Value = s.BranchCode;

					worksheet.Cells[row, 15].Value = s.UserName;
					worksheet.Cells[row, 16].Value = s.Nikname;
					//تاریخ ارجاع متصدی
					worksheet.Cells[row, 17].Value = s.DateRefEmp == null ? "-" : s.DateRefEmp.ToPersianDateTime("yyyy/MM/dd HH:mm");
					//تاریخ ارجاع رئیس شعبه
					worksheet.Cells[row, 18].Value = s.DateRefBranch == null ? "-" : s.DateRefBranch.ToPersianDateTime("yyyy/MM/dd HH:mm");


					worksheet.Cells[row, 19].Value = s.DocumentCode;
					worksheet.Cells[row, 20].Value = s.FileName;
					//Hob -> 20
					worksheet.Cells[row, 21].Value = s.UserDescription;

					worksheet.Cells[row, 22].Value = s.Status;
					worksheet.Cells[row, 23].Value = s.RegDateTime == null ? "-" : s.RegDateTime.ToPersianDateTime("yyyy/MM/dd HH:mm");
					//worksheet.Cells[row, 23].Value =  transactionToExelAll[i].Name;
					row++;
				}
				//var transactionToExelAllIm = DbSetTransactionToExelAll.Skip((index - 1) * countExport).Take(countExport);

			}

			//var transactionToExelAll = DbSetTransactionToExelAll.AsEnumerable().Select(s => new
			//{
			//	//2
			//	Id = s.Id,
			//	//MainTitleBranch = s.MainTitleBranch,
			//	MainCodeBranch = s.MainCodeBranch,
			//	TitleBranch = s.TitleBranch,
			//	MainTitleBranch = s.MainTitleBranch,
			//	//CodeBranch = s.CodeBranch,
			//	UserName = s.UserName,
			//	Nikname = s.Nikname,
			//	DateRefEmp = s.DateRefEmp,
			//	DateRefBranch = s.DateRefBranch,
			//	DocumentCode = s.DocumentCode,
			//	//Name = s.Name,
			//	UserDescription = s.UserDescription,
			//	//1
			//	FileName = s.FileName,
			//	TransactionNumber = s.TransactionNumber,
			//	Date = s.Date,
			//	Time = s.Time,
			//	CardNumber = s.CardNumber,
			//	AtmCode = s.AtmCode,
			//	Amount = s.Amount,
			//	Status = s.Status,
			//	//ATM = s.ATM,
			//	CardType = s.CardType,
			//	//3
			//	//Title = s.Title,
			//	RegDateTime = s.RegDateTime
			//}).ToList();



			//int i = 0;
			////todo
			//for (var row = 3; row <= transactionToExelAll.Count + 1; row++)
			//{
			//	worksheet.Cells[row, 1].Value = transactionToExelAll[i].Id;
			//	worksheet.Cells[row, 2].Value = transactionToExelAll[i].TransactionNumber;
			//	worksheet.Cells[row, 3].Value = transactionToExelAll[i].Date;
			//	worksheet.Cells[row, 4].Value = transactionToExelAll[i].Time;
			//	worksheet.Cells[row, 5].Value = transactionToExelAll[i].AtmCode;
			//	worksheet.Cells[row, 6].Value = transactionToExelAll[i].CardType;
			//	worksheet.Cells[row, 7].Value = transactionToExelAll[i].CardNumber;
			//	worksheet.Cells[row, 8].Value = transactionToExelAll[i].Amount;
			//	worksheet.Cells[row, 9].Value = "";//transactionToExelAll[i].ATM;
			//									   //Hob 10
			//	worksheet.Cells[row, 11].Value = transactionToExelAll[i].MainTitleBranch;
			//	worksheet.Cells[row, 12].Value = transactionToExelAll[i].MainCodeBranch;
			//	worksheet.Cells[row, 13].Value = transactionToExelAll[i].TitleBranch;

			//	worksheet.Cells[row, 14].Value = transactionToExelAll[i].UserName;
			//	worksheet.Cells[row, 15].Value = transactionToExelAll[i].Nikname;

			//	worksheet.Cells[row, 16].Value = transactionToExelAll[i].DateRefEmp;
			//	worksheet.Cells[row, 17].Value = transactionToExelAll[i].DateRefBranch;


			//	worksheet.Cells[row, 18].Value = transactionToExelAll[i].DocumentCode;
			//	worksheet.Cells[row, 19].Value = transactionToExelAll[i].FileName;
			//	//Hob -> 20
			//	worksheet.Cells[row, 20].Value = transactionToExelAll[i].Status;
			//	worksheet.Cells[row, 21].Value = transactionToExelAll[i].RegDateTime;
			//	//worksheet.Cells[row, 23].Value =  transactionToExelAll[i].Name;
			//	i++;
			//}
			worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
			package.Save();
			return content;
		}

	}
}