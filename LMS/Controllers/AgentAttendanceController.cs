using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class AgentAttendanceController : Controller
    {
        // GET: AgentAttendance
        [Authorize(Roles = "HR,Admin,SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}