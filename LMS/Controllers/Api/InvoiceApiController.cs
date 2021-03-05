using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.Models.Feature.Invoice;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers.Api
{
    public class InvoiceApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetInvoice([FromUri] GetInvoiceRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditInvoice([FromBody] EditInvoiceRequest req) 
        {
          
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddInvoice([FromBody] AddInvoiceRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object PostInvoice([FromBody] PostInvoiceRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        
    }
}