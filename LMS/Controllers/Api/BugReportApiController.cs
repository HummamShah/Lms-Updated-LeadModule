using LMS.Models.Feature.BugReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class BugReportApiController : ApiController
    {

        [HttpGet]
        public object GetListData()
        {
            var temp = new GetListingRequest();
            var result = temp.RunRequest();
            return result;
        }


        [HttpPost]
        public object AddBug([FromBody] AddBugReportRequest req) //If not working remove frombody
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        
    }
}
