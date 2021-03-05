using LMS.Models.Feature.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class BillingApiController : ApiController
    {
        [HttpGet]
        public object GetCompanyBillingInformation([FromUri] GetCompanyBillingInfomationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetBillingDetails([FromUri] GetBillingDetailsRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object UpdateBillingInformation([FromBody] UpdateCompanyBillingRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

    }
}