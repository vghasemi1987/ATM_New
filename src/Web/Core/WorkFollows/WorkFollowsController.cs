using DomainContracts.TransactionAggregate;
using KendoHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Core.WorkFollows
{
    [Authorize]
    public class WorkFollowsController : Controller
    {
        private readonly IWorkfollowRepository _workfollowRepository;

        public WorkFollowsController(IWorkfollowRepository workfollowRepository)
        {
            _workfollowRepository = workfollowRepository;
        }

        public IActionResult GetTransactionWorkFollow(string models)
        {
            var request = JsonConvert.DeserializeObject<DataSourceRequest>(models);
            var list = _workfollowRepository.GetTransactionWorkFollow(request);
            return Json(list);
        }
    }
}