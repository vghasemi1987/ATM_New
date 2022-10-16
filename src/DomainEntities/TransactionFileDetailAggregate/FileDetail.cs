using DomainEntities.Commons;
using DomainEntities.TransactionFileAggregate;
using DomainEntities.WorkfollowAggregate;
using System;
using System.Collections.Generic;

namespace DomainEntities.TransactionFileDetailAggregate
{
    public class FileDetail : Entity<int>
    {
        public FileDetail()
        {
            IsShetabiPrinted = false;
            IsDocumentPrinted = false;
            DocumentCode = 0;
        }
        public int FileId { get; set; }
        public File File { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Operation { get; set; }
        public string AtmCode { get; set; }
        public string CardNumber { get; set; }
        public decimal? Amount { get; set; }
        public string TransactionNumber { get; set; }
        public short? TypeId { get; set; }
        public Type Type { get; set; }
        public string ResponseCode { get; set; }
        public bool IsRefahi { get; set; }
        public EnumStatus? StatusId { get; set; }
        public Status Status { get; set; }
        public bool IsShetabiPrinted { get; set; }
        public DateTime? ShetabiPrintedDate { get; set; }
        public DateTime? DocumentPrintedDate { get; set; }
        public bool IsDocumentPrinted { get; set; }
        public int? DocumentCode { get; set; }
        public string UserDescription { get; set; }
        public ICollection<Workfollow> Workfollows { get; set; } = new List<Workfollow>();
    }
}