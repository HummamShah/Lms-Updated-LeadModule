using LMS.Models.Feature.Charge;
using LMS.Models.Feature.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class ChargeApiController : ApiController
    {

        [HttpGet]
        public object GetListData([FromUri] Models.Feature.Charge.GetListingRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCompaniesAccountDropdown([FromUri] GetCompaniesAccountDropdownRequest req)
        {
            // var req = new GetCompaniesAccountDropdownRequest();
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]
        public object GetCharge([FromUri] GetChargeRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddCharge([FromBody] AddChargeRequest req) 
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditCharge([FromBody] EditChargeRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]
        public object GetChargesByType([FromUri] GetChargesByTypeRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
    }
}