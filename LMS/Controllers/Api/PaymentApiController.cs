using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LMS.Models.Feature.Payment;

namespace LMS.Controllers.Api
{
    public class PaymentApiController : ApiController
    {

        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetPayment([FromUri] GetPaymentRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddPayment([FromBody] AddPaymentRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditPayment([FromBody] EditPaymentRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object PostPayment([FromBody] PostPaymentRequest req)
        {
            
            var result = req.RunRequest(req);
            return result;
        }
    }
}