using DomainEntities.ApplicationUserAggregate;
using DomainEntities.BranchAggregate;
using DomainEntities.Commons;
using DomainEntities.TransactionFileDetailAggregate;
using System;
using System.Collections.Generic;

namespace DomainEntities.TransactionFileAggregate
{
    public class File : Entity<int>
    {
        public File()
        {
            RegDateTime = DateTime.Now;
        }
        public string Name { get; set; }
        public byte[] FileData { get; set; }
        public EnumStatus? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime RegDateTime { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int BranchHeadId { get; set; }
        public ICollection<FileDetail> FileDetails { get; set; } = new List<FileDetail>();
    }
}