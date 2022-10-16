using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.TransactionAggregate;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using File = DomainEntities.TransactionFileAggregate.File;
using ApplicationCommon;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.WorkfollowAggregate;

namespace DomainApplication.Services.TransactionAggregate
{
    public class TransactionService : ITransactionService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileDetailRepository _fileDetailRepository;
        private readonly IWorkfollowRepository _workfollowRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _unitOfWork;


        public TransactionService(IFileRepository agentRepository,
            IFileDetailRepository fileDetailRepository,
            IWorkfollowRepository workfollowRepository,
            IHostingEnvironment hostingEnvironment,
            IUnitOfWork unitOfWork)
        {
            _fileRepository = agentRepository;
            _fileDetailRepository = fileDetailRepository;
            _workfollowRepository = workfollowRepository;
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<File>> GetAllAsync()
        {
            return await _fileRepository.ListAllAsync();
        }

        public File FindByIdAsync(int id)
        {
            return _fileRepository.GetSingleBySpec(o => o.Id == id);
        }

        public string SendToStatus(List<int> transactionIds, EnumStatus status, string messageText, int userId)
        {
            var lastDocumentCode = 0;
            if (status == EnumStatus.SendToBranchBoss && status != EnumStatus.BackToOperator)
            {
                lastDocumentCode = _fileDetailRepository.FindMaxDocumentCode();
            }
            var list = _fileDetailRepository.GetListById(transactionIds);
            var sixMonthBackDate = DateTime.Now.AddDays(-180);
            foreach (var fileDetail in list)
            {
                if (fileDetail.IsRefahi == true)
                {
                    return "DateIsRefahi";
                }
                DateTime arraydate = (fileDetail.Date.Substring(0, 4) + "/" + fileDetail.Date.Substring(4, 2) + "/"
                    + fileDetail.Date.Substring(6, 2)).ToMiladiDate();
                if (arraydate < sixMonthBackDate)
                {
                    return "DateBigSiXMonth";
                }
            }
            if (list != null)
            {
                var fileId = list.First().FileId;
                var file = _fileRepository.GetSingleBySpec(o => o.Id == fileId);
                file.StatusId = status;
                _fileRepository.Update(file, f => f.StatusId);
                _unitOfWork.Save();

            }
            foreach (var fileDetail in list)
            {
                if (status == EnumStatus.SendToBranchBoss && status != EnumStatus.BackToOperator)
                {
                    fileDetail.DocumentCode = lastDocumentCode;
                }
                fileDetail.StatusId = status;
                fileDetail.UserDescription = messageText;
                _fileDetailRepository.Update(fileDetail, o => o.StatusId, o => o.DocumentCode, o => o.UserDescription);
            }

            var workfollows = transactionIds.Select(o => new Workfollow { FileDetailId = o, StatusId = status, UserId = userId });
            _workfollowRepository.AddList(workfollows);
            _unitOfWork.Save();
            return "ok";
        }

        public async Task Save(File file)
        {
            _fileRepository.Add(file);
            //var status = file.FileDetails.Select(o => new Workfollow { FileDetailId = o.Id, StatusId = o.StatusId });
            //_workfollowRepository.AddList(status);
            await _unitOfWork.SaveAsync();
        }

        public async Task<byte[]> GetShetabiFile(List<int> data)
        {
            var dataList = _fileDetailRepository.GetListById(data);
            dataList = dataList.Where(o => o.IsRefahi == false);
            var all = 0;
            var stringWithYourData = "";
            var i = 0;
            var listWorkfollow = new List<Workfollow>();
            foreach (var item in dataList)
            {
                var parsed = int.TryParse(item.Amount.ToString().PadLeft(12, '0'), out var numValue);

                if (parsed)
                    all += numValue;

                item.StatusId = EnumStatus.FinalRegistration;
                item.IsShetabiPrinted = true;
                item.ShetabiPrintedDate = DateTime.Now;
                _fileDetailRepository.Update(item, o => o.StatusId, o => o.IsShetabiPrinted, o => o.ShetabiPrintedDate);
                i += 1;
                var str = "";
                for (var j = 0; j < 6 - i.ToString().Length; j++)
                    str += "0";

                str += i.ToString();
                stringWithYourData += str;
                stringWithYourData += "/2/1/";
                stringWithYourData += item.Date;
                stringWithYourData += "/";
                stringWithYourData += item.TransactionNumber.Trim();
                stringWithYourData += "/";
                stringWithYourData += item.CardNumber.Trim();
                stringWithYourData += "   ";
                stringWithYourData += "/";
                stringWithYourData += item.Amount.ToString().PadLeft(12, '0');
                stringWithYourData += "/";
                stringWithYourData += item.CardNumber.Substring(0, 6).Trim();
                stringWithYourData += "/1/589463/2/ATMWD/";
                stringWithYourData += item.Amount.ToString().PadLeft(12, '0');
                stringWithYourData += "/00/";
                stringWithYourData += item.Time;
                stringWithYourData += "/";
                stringWithYourData += item.AtmCode.Trim();
                stringWithYourData += "/0";
                stringWithYourData += "***";

                listWorkfollow.Add(new Workfollow { FileDetailId = item.Id, StatusId = item.StatusId });
                item.IsShetabiPrinted = true;
                item.ShetabiPrintedDate = DateTime.Now;
                _fileDetailRepository.Update(item, o => o.IsShetabiPrinted, o => o.ShetabiPrintedDate);
            }

            _workfollowRepository.AddList(listWorkfollow);
            await _unitOfWork.SaveAsync();

            //var countZero = "";
            //for (var j = 0; j < 12 - all.ToString().Length; j++)
            //    countZero += "0";

            var allAmount = all.ToString().PadLeft(12, '0');

            //var str2 = "";
            //for (var j = 0; j < 6 - i.ToString().Length; j++)
            //    str2 += "0";

            stringWithYourData += i.ToString().PadLeft(6, '0');
            stringWithYourData += "/0/0/000000/000000/0000000000000000   /" + allAmount + "/000000/0/000000/0/00/" +
                                    allAmount + "/00000/000000/00000000/0***";
            stringWithYourData = stringWithYourData.Replace("***", "\r\n");
            stringWithYourData = stringWithYourData.Remove(stringWithYourData.Length - 2, 2);
            return Encoding.ASCII.GetBytes(stringWithYourData);
        }

        //private static string ToStrAmountFormat12(string s)
        //{
        //var count = 12 - s.Length;
        //for (var i = 0; i < count; i++)
        //{
        //    s = "0" + s;
        //}
        //return s;
        //}

        public async Task<bool> CheckTransactionExist(string cardNumber, string transactionNumber)
        {
            var list = await _fileDetailRepository.GetByCardNumberAndTransactionNumber(
                cardNumber.Trim(),
                transactionNumber.Trim());
            return list == null;
        }

        public async Task<MemoryStream> GenerateFileDetailExcelReport(List<int> ids)
        {
            MemoryStream content = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(content))
            {
                var dataList = await _fileRepository.GetFileTransaction(ids);
                var worksheet = package.Workbook.Worksheets.Add("فزونی");

                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells["A1:H1"].Value = "اطلاعات تراکنش";

                worksheet.Cells["J1:T1"].Merge = true;
                worksheet.Cells["J1:T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["J1:T1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells["J1:T1"].Value = "اطلاعات شعبه عامل و ارسال فایل";

                worksheet.Cells["V1:W1"].Merge = true;
                worksheet.Cells["V1:W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["V1:W1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells["V1:W1"].Value = "اقدام انجام شده";
            }
            return content;
        }

        public async Task<MemoryStream> GenerateExcelReport(List<int> ids)
        {
            //var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "ExcelExport.xlsx");
            //var file = new FileInfo(filePath);
            //if (file.Exists)
            //file.Delete();
            MemoryStream content = new MemoryStream();
            using (var package = new ExcelPackage(content))
            {
                var worksheet = package.Workbook.Worksheets.Add("Main");

                worksheet.Cells["C:C"].Style.Numberformat.Format = "#,###,###";
                worksheet.Cells["B:B"].Style.Numberformat.Format = "@";
                worksheet.Cells["A:F"].Style.Font.Bold = true;
                worksheet.View.RightToLeft = true;
                worksheet.Cells["A:F"].Style.Font.Size = 16;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 40;
                worksheet.Column(5).Width = 40;
                worksheet.Column(6).Width = 40;
                worksheet.Cells["A:F"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A1:F1"].Value = "اداره حسابداری واحد عملیات مالی سامانه متمرکز";

                worksheet.Cells["A2:F2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A2:F2"].Value = "فزونی خودپرداز";

                worksheet.Cells["A3:F3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A3:F3"].Merge = true;
                worksheet.Cells["A3:F3"].Value = "تاریخ فزونی :" + CustomDateTime.CurrentPersianDate();

                worksheet.Cells["A4:F4"].Merge = true;

                worksheet.Cells["A5:F5"].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                worksheet.Cells["A5:F5"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                worksheet.Cells["A5:F5"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                worksheet.Cells["A5:F5"].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                worksheet.Cells["A5"].Value = "ردیف";
                worksheet.Cells["B5"].Value = "شماره سند";
                worksheet.Cells["C5"].Value = "شماره تراکنش";
                worksheet.Cells["D5"].Value = "کد شعبه";
                worksheet.Cells["E5"].Value = "شماره حساب";
                worksheet.Cells["F5"].Value = "مبلغ";


                var j = 5;
                decimal allAmount = 0;

                var dataItems = await _fileDetailRepository.GetTransactionByDocumentCode(ids);
                foreach (var item in dataItems)
                {
                    allAmount += item.Amount;

                    worksheet.Cells["A" + (j + 1)].Value = (j - 4).ToString();
                    worksheet.Cells["B" + (j + 1)].Value = item.Id;
                    worksheet.Cells["C" + (j + 1)].Value = item.TransactionNumber;
                    worksheet.Cells["D" + (j + 1)].Value = item.BranchCode;
                    worksheet.Cells["E" + (j + 1)].Value = item.AccountNo;
                    worksheet.Cells["F" + (j + 1)].Value = item.Amount;

                    worksheet.Cells["A" + (j + 1) + ":F" + (j + 1)].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells["A" + (j + 1) + ":F" + (j + 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells["A" + (j + 1) + ":F" + (j + 1)].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    worksheet.Cells["A" + (j + 1) + ":F" + (j + 1)].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                    j++;

                    var dataList = await _fileDetailRepository.GetListByDocumentCode(item.Id);
                    foreach (var itemdl in dataList)
                    {
                        itemdl.IsDocumentPrinted = true;
                        itemdl.DocumentPrintedDate = DateTime.Now;
                        _fileDetailRepository.Update(itemdl, o => o.IsDocumentPrinted, o => o.DocumentPrintedDate);
                    }
                    await _unitOfWork.SaveAsync();
                }
                j++;

                worksheet.Cells["A" + j + ":E" + j].Merge = true;
                worksheet.Cells["A" + j + ":E" + j].Value = "جمع کل :";
                worksheet.Cells["F" + j].Value = allAmount;

                j++;
                worksheet.Cells["A" + j + ":F" + j].Merge = true;
                j++;
                worksheet.Cells["A" + j + ":F" + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A" + j + ":F" + j].Merge = true;
                worksheet.Cells["A" + j + ":F" + j].Value = "خواهشمند است نسبت به منظور نمودن مبالغ فوق به بدهکار حساب شعب مربوطه اقدام فرمایید .";
                j++;
                worksheet.Cells["A" + j + ":F" + j].Merge = true;
                j++;
                worksheet.Cells["A" + j + ":F" + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A" + j + ":F" + j].Merge = true;
                worksheet.Cells["A" + j + ":F" + j].Value = "مسئول واحد                         کاربر  :";

                package.Save();


            }
            return content;
        }
    }
}
