using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
   
    public class EditPurchaseOrderResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class EditPurchaseOrderRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public int Status { get; set; }
        public int? LeadId { get; set; }
        public int? CompanyId { get; set; }
        public int BillingStatus { get; set; }
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

        public object RunRequest(EditPurchaseOrderRequest request)
        {
            var response = new EditPurchaseOrderResponse();
            var PurchaseOrder = _dbContext.PurchaseOrder.Where(x=>x.Id == request.Id).FirstOrDefault();
            PurchaseOrder.AssignedCoreAgentId = request.AssignedCoreAgentId;
            PurchaseOrder.AssignedDepartmentId = request.AssignedDepartmentId;
            PurchaseOrder.AssignedPresalesAgentId = request.AssignedPresalesAgentId;
            PurchaseOrder.AssignedSDAgentId = request.AssignedSDAgentId;
            PurchaseOrder.BillingStatus = request.BillingStatus;
            PurchaseOrder.CompanyId = request.CompanyId;
            PurchaseOrder.CoreProcessStatus = (int)Enums.ProcessStatus.Pending;
            PurchaseOrder.Description = request.Description;
            PurchaseOrder.LeadId = request.LeadId;
            PurchaseOrder.MRC = request.MRC;
            PurchaseOrder.OTC = request.OTC;
            PurchaseOrder.Package = request.Package;
            PurchaseOrder.PresalesProcessStatus = (int)Enums.ProcessStatus.Pending;
            PurchaseOrder.SdProcessStatus = (int)Enums.ProcessStatus.Pending;
            PurchaseOrder.Status = (int)PurchaseOrderStatus.Inprogress;
            PurchaseOrder.VendorProcessStatus = (int)Enums.ProcessStatus.Pending;
            PurchaseOrder.Date = DateTime.Now.Date;
            PurchaseOrder.CreatedAt = DateTime.Today;
            PurchaseOrder.CreatedBy = request.CreatedBy;
            _dbContext.SaveChanges();
            return response;
        }
    }
}