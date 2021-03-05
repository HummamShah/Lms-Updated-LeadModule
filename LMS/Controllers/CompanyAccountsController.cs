using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class CompanyAccountsController : Controller
    {
        // GET: CompanyAccounts
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Detail()
        {
            return View();
        }
        [Authorize]
        public ActionResult ParentCompanies()
        {
            return View();
        }
        [Authorize]
        public ActionResult ChildCompanies()
        {
            return View();
        }
    }
}