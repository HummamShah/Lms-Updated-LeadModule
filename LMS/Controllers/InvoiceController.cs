using LMS.Models.Feature.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        // GET: Invoice
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
        [Authorize]
        public ActionResult Detail()
        {
            return View();
        }
        [Authorize]
        public ActionResult Print(int Id,string type)
        {
            if (type == "Sales")
            {
                ViewBag.IsSalesTax = true;
            }
            else
            {
                ViewBag.IsSalesTax = false;
            }
            var req = new GetInvoiceRequest { Id=Id};
            var resp = req.RunRequest(req);
            return View(resp);
        }
    }
}