using DomainContracts.ATMUnknownTransactionsAggregate;
using DomainContracts.Commons;
using DomainEntities.ATMUnknownTransactionsAggregate;
using DomainEntities.TransactionFileAggregate;
using System;
using System.Collections.Generic;

namespace DomainApplication.Services.ATMUnknownTransactionsAggregate
{
    public class ATMUnknownTransactionsService : IATMUnknownTransactionsService
    {
        private readonly IATMUnknownTransactionsRepository _transactionsRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ATMUnknownTransactionsService(
            IATMUnknownTransactionsRepository transactionsRepository,
            IUnitOfWork unitOfWork)
        {
            _transactionsRepository = transactionsRepository;
            _unitOfWork = unitOfWork;
        }
        public void SendToStatus(List<int> transactionIds, EnumStatus status, string messageText, int userId, int customerOrBreanchId)
        {

            var list = _transactionsRepository.GetListById(transactionIds);
            var listWorkfollows = new List<WorkfollowATMUnknownTransactions>();
            foreach (var aTMUnknown in list)
            {
                if (status == EnumStatus.SendToBranchBoss && status != EnumStatus.BackToOperator)
                {

                }
                aTMUnknown.StatusWorkfollowId = status;
                aTMUnknown.UserDescription = messageText;
                aTMUnknown.StatusID = customerOrBreanchId;
                aTMUnknown.Status = customerOrBreanchId == 1300 ? "سهم شعبه" : "سهم مشتری";
                aTMUnknown.IsManually = status == EnumStatus.FinalRegistration ? true : false;
                aTMUnknown.ManualResolveDate = status == EnumStatus.FinalRegistration ? DateTime.Now : DateTime.MinValue;
                _transactionsRepository.Update(aTMUnknown
                    , o => o.StatusWorkfollowId
                    , o => o.UserDescription
                    , o => o.StatusID
                    , o => o.Status
                    , o => o.IsManually
                    , o => o.ManualResolveDate);
                listWorkfollows.Add(new WorkfollowATMUnknownTransactions
                {
                    ATMUnknownTransactionsId = aTMUnknown.Id,
                    Status = customerOrBreanchId == 1300 ? "سهم شعبه" : "سهم مشتری",
                    StatusID = customerOrBreanchId,
                    StatusWorkfollowId = (short)status,
                    UserId = userId,
                    UserMessage = messageText
                });
            }
            _transactionsRepository.AddWorkfollows(listWorkfollows);
            _unitOfWork.Save();
        }
    }
}
