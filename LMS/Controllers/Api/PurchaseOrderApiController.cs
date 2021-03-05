using LMS.Models.Enums;
using LMS.Models.Feature.PurchaseOrder;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class PurchaseOrderApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
           req.IsPmd = User.IsInRole(Roles.Pmd_Feasibility);
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetPurchaseOrder([FromUri] GetPurchaseOrderRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddPurchaseOrder([FromBody] AddPurchaseOrderRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EditPurchaseOrder([FromBody] EditPurchaseOrderRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AssignDepartment([FromBody] AssignDepartmentRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ApprovePurchaseOrder([FromBody] ApprovePurchaseOrderRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object UnAssignPurchaseOrder([FromBody] UnAssignPurchaseOrderRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddVendorSuggestionPurchaseOrder([FromBody] AddVendorSuggestionPurchaseOrderRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddCoreIpPurchaseOrder([FromBody] AddCoreIpPurchaseOrderRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddClientVerification([FromBody] AddClientVerificationRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object ChangePurchaseOrderStatus([FromBody] ChangePurchaseOrderStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ChangePurchaseOrderBillingStatus([FromBody] ChangePurchaseOrderBillingStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}