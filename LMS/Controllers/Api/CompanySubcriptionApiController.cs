using LMS.Models.Feature.CompanySubcription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class CompanySubcriptionApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddCompanySubcription([FromBody] AddCompanySubcriptionRequest req) //If not working remove frombody
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
    }
}