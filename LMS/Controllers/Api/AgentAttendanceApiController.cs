using LMS.Models.Feature.AgentAttendance;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class AgentAttendanceApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddAgentAttendance([FromBody] AddAgentAttendanceRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object SignOffAttendance([FromBody] SignOffAttendanceRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

    }
}