using DomainEntities.TransactionFileAggregate;
using System;

namespace DomainEntities.TransactionFileDetailAggregate
{
    public class FileDetailListDto
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Operation { get; set; }
        public string AtmCode { get; set; }
        public string CardNumber { get; set; }
        public decimal? Amount { get; set; }
        public string TransactionNumber { get; set; }
        public string ResponseCode { get; set; }
        public bool IsRefahi { get; set; }
        public EnumStatus? StatusId { get; set; }
        public string StatusTitle { get; set; }
        public bool IsShetabiPrinted { get; set; }
        public int? DocumentCode { get; set; }
        public bool IsDocumentPrinted { get; set; }
        public string UserDescription { get; set; }
        public string BranchName { get; set; }
        public int? BranchCode { get; set; }
       public int BranchId { get; set; }
        public string UserName { get; set; }
       public int UserId { get; set; }
        public string ReferenceDateBoss { get; set; }
        public string ReferenceDateOperator { get; set; }
        public DateTime? ShetabiPrintedDate { get; set; }
        public DateTime? DocumentPrintedDate { get; set; }
        public string FileName { get; set; }
    }
}
