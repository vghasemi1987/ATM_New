using System;
using System.Collections.Generic;
using System.Text;

namespace DomainEntities.ATMUnknownTransactionsAggregate
{
    public class WorkfollowATMUnknownTransactionsDto
    {
        public int Id { get; set; }
        public string CreateDateTime { get; set; }
        public string User{ get; set; }
        public string Status { get; set; }//50
        public string UserMessage { get; set; }
    }
}
