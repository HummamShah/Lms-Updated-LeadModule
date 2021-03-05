using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class LeadController : Controller
    {
        // GET: Lead
        [Authorize(Roles = "Lead_Sales,Admin,SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Pmd_Feasibility,PreSale")]
        public ActionResult List()
        {
            return View();
        }
        [Authorize(Roles = "Lead_Sales,Admin,SuperAdmin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin,SuperAdmin,Pmd_Feasibility")]
        public ActionResult AddFeasibility()
        {
            return View();
        }

        [Authorize(Roles = "Lead_Sales,Admin,SuperAdmin,Pmd_Feasibility,PreSale")]
        public ActionResult Detail()
        {
            return View();
        }

        [Authorize(Roles = "Lead_Sales,Admin,SuperAdmin")]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Pmd_Feasibility")]
        public ActionResult EditFeasibility()
        {
            return View();
        }
        [Authorize(Roles = "PreSale")]
        public ActionResult AddCommercial ()
        {
            return View();
        }
        //[Authorize(Roles = "PreSale")]
        //public ActionResult EditCommercial()
        //{
        //    return View();
        //}
        [Authorize]
        public ActionResult Questionare()
        {
            return View();
        }
    }
}