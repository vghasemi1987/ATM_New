using DomainEntities.ATMUnknownTransactionsAggregate;
using System.Collections.Generic;

namespace Web.Core.ATMUnknown.ViewModels
{
    public class ListWorkfollowViewModel
    {
        public List<WorkfollowATMUnknownTransactionsDto> Workfollows { get; set; }
    }
}
