using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    [Authorize]
    public class BillingController : Controller
    {
        // GET: Billing
        [Authorize]
        public ActionResult Index()
        {
            //List of all companies
            return View();
        }
        [Authorize]
        public ActionResult Details()
        {
            return View();
        }
        [Authorize]
        public ActionResult UpdateBilling()
        {
            //Option to add new or update new billing information (Per company)
            return View();
        }
        [Authorize]
        public ActionResult AccountStatements()
        {
            //List of Companies whose account/Billing infromation has been created
            return View();
        }
        [Authorize]
        public ActionResult AccountStatementDetails()
        {
            
            return View();
        }
    }
}