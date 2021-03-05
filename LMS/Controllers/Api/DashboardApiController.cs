using LMS.Models.Enums;
using LMS.Models.Feature.UserReport;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class DashboardApiController : ApiController
    {
        [HttpGet]
        public object GetUserReportData([FromUri] GetUserReportDataRequest req)
        {
            if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.SuperAdmin))
            {
                req.UserId = User.Identity.GetUserId();
            }
            var result = req.RunRequest(req);
            return result;         
        }

    }
}