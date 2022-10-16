using DomainEntities.Commons;
using System;

namespace DomainEntities.TransactionFileAggregate
{
    public class FileListDto : Entity<int>
    {
        public string Name { get; set; }
        public EnumStatus? StatusId { get; set; }
        public DateTime RegDateTime { get; set; }
        public string UserName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int BranchCode { get; set; }
        public int BranchHeadId { get; set; }
        public string LastStatusTitle { get; set; }


        //public string Date { get; set; }
        //public string Time { get; set; }
        //public string Operation { get; set; }
        //public string AtmCode { get; set; }
        //public string CardNumber { get; set; }
        //public decimal? Amount { get; set; }
        //public string TransactionNumber { get; set; }
        //public string ResponseCode { get; set; }
        //public bool IsRefahi { get; set; }
        //public string StatusTitle { get; set; }
      
        //public int? DocumentCode { get; set; }
        //public bool IsDocumentPrinted { get; set; }
        //public string UserDescription { get; set; }
        //public int UserId { get; set; }
        //public string ReferenceDateBoss { get; set; }
        //public string ReferenceDateOperator { get; set; }
        //public DateTime? DocumentPrintedDate { get; set; }

    }
}