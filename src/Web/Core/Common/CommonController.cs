using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core.Common
{
    [Authorize]
    [DisplayName("کارهای عمومی")]
    public class CommonController : Controller
    {
        public CommonController()
        {
        }
    }
}