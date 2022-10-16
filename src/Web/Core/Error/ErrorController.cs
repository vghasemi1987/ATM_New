using System;
using Infrastructure.Data.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Core.Error
{
    [Route("[controller]/[action]")]
    public class ErrorController : Controller
    {
        private readonly AtmContext _crmContext;
        public ErrorController(AtmContext crmContext)
        {
            _crmContext = crmContext;
        }
        [HttpGet("{status}")]
        public IActionResult Code(string status)
        {
            switch (status)
            {
                case "403":
                    return View(new Tuple<string, string>("403", "شماه اجازه دسترسی به این بخش را ندارید!"));
                case "404":
                    return View(new Tuple<string, string>("404", "صفحه درخواست شده وجود ندارد!"));
            }
            return View(new Tuple<string, string>("400", "خطای غیرمنتظره ای رخ داده است!"));
        }

        public string AddDb()
        {
            _crmContext.Database.Migrate();
            return "OK!";
        }
    }
}