using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
    
    public class GetPurchaseOrderResponse
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string StatusEnum { get; set; }
        public int? LeadId { get; set; }
        public int? CompanyId { get; set; }
        public int BillingStatus { get; set; }
        public string BillingStatusEnum { get; set; }
        public int? AssignedDepartmentId { get; set; }
        public int? AssignedCoreAgentId { get; set; }
        public int? AssignedSDAgentId { get; set; }
        public int? AssignedPresalesAgentId { get; set; }
        public decimal OTC { get; set; }
        public decimal MRC { get; set; }
        public string Package { get; set; }
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? Date { get; set; }
        public int? CoreProcessStatus { get; set; }
        public int? PresalesProcessStatus { get; set; }
        public int? SdProcessStatus { get; set; }
        public int? VendorProcessStatus { get; set; }


        public int? PresalesApproval { get; set; }
        public string PresalesApprovalEnum { get; set; }
        public int? SDSuggestedVendorId1 { get; set; }
        public string SDSuggestedVendorName1 { get; set; }
        public int? SDSuggestedVendorId2 { get; set; }
        public string SDSuggestedVendorName2 { get; set; }
        public int? SDSuggestedVendorId3 { get; set; }
        public string SDSuggestedVendorName3 { get; set; }
        public string CoreIp { get; set; }
        public string CoreVlanId_ { get; set; }
        public bool? SDClientVerification { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class GetPurchaseOrderRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
  

        public object RunRequest(GetPurchaseOrderRequest request)
        {
            var response = new GetPurchaseOrderResponse();
            var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
            response.Id = PurchaseOrder.Id;
            response.AssignedCoreAgentId = PurchaseOrder.AssignedCoreAgentId;
            response.AssignedDepartmentId = PurchaseOrder.AssignedDepartmentId;
            response.AssignedPresalesAgentId = PurchaseOrder.AssignedPresalesAgentId;
            response.AssignedSDAgentId = PurchaseOrder.AssignedSDAgentId;
            response.BillingStatus = PurchaseOrder.BillingStatus;
            response.BillingStatusEnum = ((PurchaseOrderBillingStatus)PurchaseOrder.BillingStatus).ToString();
            response.CompanyId = PurchaseOrder.CompanyId;
          
            response.Description = PurchaseOrder.Description;
            response.LeadId = PurchaseOrder.LeadId;
            response.MRC = PurchaseOrder.MRC;
            response.OTC = PurchaseOrder.OTC;
            response.Package = PurchaseOrder.Package;

            response.CoreProcessStatus = PurchaseOrder.CoreProcessStatus;
            response.PresalesProcessStatus = PurchaseOrder.PresalesProcessStatus;
            response.SdProcessStatus = PurchaseOrder.SdProcessStatus;
            response.Status = PurchaseOrder.Status;
            response.StatusEnum = ((PurchaseOrderStatus)PurchaseOrder.Status).ToString();
            response.VendorProcessStatus = PurchaseOrder.VendorProcessStatus;

            response.PresalesApproval = PurchaseOrder.PresalesApproval;
            if (PurchaseOrder.PresalesApproval.HasValue)
            {
                response.PresalesApprovalEnum = ((Approval)PurchaseOrder.PresalesApproval.Value).ToString();
            }
            
            response.SDClientVerification = PurchaseOrder.SDClientVerification;
            response.SDSuggestedVendorId1 = PurchaseOrder.SDSuggestedVendorId1;
            if (PurchaseOrder.SDSuggestedVendorId1.HasValue)
            {
                response.SDSuggestedVendorName1 = _dbContext.Vendor.Where(x => x.Id == PurchaseOrder.SDSuggestedVendorId1).FirstOrDefault().Name;
            }
           
            response.SDSuggestedVendorId2 = PurchaseOrder.SDSuggestedVendorId2;
            if (PurchaseOrder.SDSuggestedVendorId2.HasValue)
            {
                response.SDSuggestedVendorName2 = _dbContext.Vendor.Where(x => x.Id == PurchaseOrder.SDSuggestedVendorId2).FirstOrDefault().Name;
            }
            response.SDSuggestedVendorId3 = PurchaseOrder.SDSuggestedVendorId3;
            if (PurchaseOrder.SDSuggestedVendorId3.HasValue)
            {
                response.SDSuggestedVendorName3 = _dbContext.Vendor.Where(x => x.Id == PurchaseOrder.SDSuggestedVendorId3).FirstOrDefault().Name;
            }
            response.CoreIp = PurchaseOrder.CoreIp;
            response.CoreVlanId_ = PurchaseOrder.CoreVlanId_;
            response.Date = PurchaseOrder.Date;
            response.CreatedAt = PurchaseOrder.CreatedAt;
            response.CreatedBy = PurchaseOrder.CreatedBy;
            return response;
        }
    }
}