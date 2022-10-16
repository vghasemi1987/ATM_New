using DomainEntities.TransactionFileAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainContracts.ATMUnknownTransactionsAggregate
{
    public interface IATMUnknownTransactionsService
    {
        void SendToStatus(List<int> transactionIds, EnumStatus status, string messageText, int userId,int CustomerOrBreanchId);
    }
}
