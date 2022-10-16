using DomainEntities.TransactionFileDetailAggregate;
using System;

namespace DomainEntities.WorkfollowAggregate
{
    public class WorkfollowDetailDto
    {
        public int Id { get; set; }
        public int? FileDetailId { get; set; }
        //public int? StatusId { get; set; }
        public string Status { get; set; }
        public DateTime UpdateDateTime { get; set; }
        //public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}