using LMS.Models.Enums;
using LMS.Models.Feature.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class VendorApiController : ApiController
    {
        [HttpGet]
        public object GetListData()
        {
            var temp = new GetListingRequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpGet]
        public object GetVendors()
        {
            var temp = new GetVendorsRequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpPost]
        public object AddVendor([FromBody] AddVendorRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetVendorById([FromUri] GetVendorByIdRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditVendor([FromBody] EditVendorRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

    }
}