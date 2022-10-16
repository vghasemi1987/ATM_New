using DomainContracts.TransactionAggregate;

namespace DomainApplication.Services.TransactionAggregate
{
    public class WorkfollowService : IWorkfollowService
    {
        private readonly IWorkfollowRepository _workfollowRepository;

        public WorkfollowService(IWorkfollowRepository workfollowRepository)
        {
            _workfollowRepository = workfollowRepository;
        }

        //public void SendToStatus(List<int> transactionIds, int statusId)
        //{
        //    try
        //    {
        //        var workfollows = new List<Workfollow>();
        //        foreach (var transactionId in transactionIds)
        //        {
        //            workfollows.Add(new Workfollow{FileDetailId = transactionId, StatusId = statusId});
        //        }
        //        _workfollowRepository.AddList(workfollows);
        //        _unitOfWork.Save();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
            
        //}
    }
}
