using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        // GET: Comapny
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Edit2()
        {
            return View();
        }
        [Authorize]
        public ActionResult Detail()
        {
            return View();
        }

        [Authorize]
        public ActionResult Parent()
        {
            return View();
        }
        [Authorize]
        public ActionResult Children()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddParent()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddChildren()
        {
            return View();
        }
        public ActionResult Billing()
        {
            return View();
        }
    }
}