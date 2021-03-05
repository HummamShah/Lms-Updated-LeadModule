using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProcessStatus = LMS.Models.Enums.ProcessStatus;

namespace LMS.Models.Feature.PurchaseOrder
{
    public class GetListingResponse
    {
        public List<PurchaseOrderData> Data { get; set; }
    }

    public class PurchaseOrderData
    {
        public int Id { get; set; }

        //Statuses
        public int Status { get; set; }
        public string StatsuEnum { get; set; }
        public int? CoreProcessStatus { get; set; }
        public string CoreProcessStatusEnum { get; set; }
        public int? SdProcessStatus { get; set; }
        public string SdProcessStatusEnum { get; set; }
        public int? PresalesProcessStatus { get; set; }
        public string PresalesProcessStatusEnum { get; set; }
        public int? VendorProcessStatus { get; set; }
        public string VendorProcessStatusEnum { get; set; }
        public int BillingStatus { get; set; }
        public string BillingStatusEnum { get; set; }


        //Lead/Company Referal
        public int? LeadId { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }


      //Assignments
        public int? AssignedDepartmentId { get; set; }
        public string AssignedDepartmentName { get; set; }
        public int? AssignedCoreAgentId { get; set; }
        public string AssignedCoreAgentName { get; set; }
        public int? AssignedSDAgentId { get; set; }
        public string AssignedSDAgentName { get; set; }
        public int? AssignedPresalesAgentId { get; set; }
        public string AssignedPresalesAgentName { get; set; }


        public string CurrentlyAssignedTo { get; set; }


        public decimal OTC { get; set; }
        public decimal MRC { get; set; }
        public string Package { get; set; }
	    public string Description { get; set; }

            //Inputs
                public int? PresalesApproval { get; set; }
        public string PresalesApprovalEnum { get; set; }
        public int? SDSuggestedVendorId1 { get; set; }
        public int? SDSuggestedVendorId2 { get; set; }
        public int? SDSuggestedVendorId3 { get; set; }
        public string CoreIp { get; set; }
        public string CoreVlanId_ { get; set; }
        public bool? SDClientVerification { get; set; }

        //DateTime
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Date { get; set; }
    }

    public class GetListingRequest
    {
        public bool IsPmd { get; set; }
        public string UserId { get; set; }
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<PurchaseOrderData>();
            var Data = _dbContext.PurchaseOrder.ToList();
            if (!req.IsPmd)
            {
                var DepartmentId = 0;
                var agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
                if (agent != null)
                {
                    if (agent.DepartmentId.HasValue)
                    {
                        DepartmentId = agent.DepartmentId.Value;
                        Data = Data.Where(x => x.AssignedDepartmentId == DepartmentId).ToList();
                    }
                   
                }
               
            }

            foreach (var d in Data)
            {
                var PurchaseOrder = new PurchaseOrderData();
                PurchaseOrder.Id = d.Id;

                //Assignments
                PurchaseOrder.AssignedCoreAgentId = d.AssignedCoreAgentId;
                if (d.AssignedCoreAgentId.HasValue)
                {
                    PurchaseOrder.AssignedCoreAgentName = d.CoreAgent.FisrtName + " " + d.CoreAgent.LastName;
                }
                PurchaseOrder.AssignedDepartmentId = d.AssignedDepartmentId;
                if (d.AssignedDepartmentId.HasValue)
                {
                    PurchaseOrder.AssignedDepartmentName = ((Departments)d.AssignedDepartmentId.Value).ToString();
                }
                PurchaseOrder.AssignedPresalesAgentId = d.AssignedPresalesAgentId;
                if (d.AssignedPresalesAgentId.HasValue)
                {
                    PurchaseOrder.AssignedPresalesAgentName = d.PresalesAgent.FisrtName + " " + d.PresalesAgent.LastName;
                }
                PurchaseOrder.AssignedSDAgentId = d.AssignedSDAgentId;
                if (d.AssignedSDAgentId.HasValue)
                {
                    PurchaseOrder.AssignedSDAgentName = d.SDAgent.FisrtName + " " + d.SDAgent.LastName;
                }


                //Referals
                PurchaseOrder.LeadId = d.LeadId;
                PurchaseOrder.CompanyId = d.CompanyId;
                if (PurchaseOrder.CompanyId.HasValue)
                {
                    PurchaseOrder.CompanyName = d.Company.Name;
                }


                PurchaseOrder.Description = d.Description;
                PurchaseOrder.MRC = d.MRC;
                PurchaseOrder.OTC = d.OTC;
                PurchaseOrder.Package = d.Package;


                //Statuses
                PurchaseOrder.BillingStatus = d.BillingStatus;
                PurchaseOrder.BillingStatusEnum = ((PurchaseOrderBillingStatus)d.BillingStatus).ToString();
                PurchaseOrder.Status = d.Status;
                PurchaseOrder.StatsuEnum = ((PurchaseOrderStatus)d.Status).ToString();
                //ProcessStsatuses
                PurchaseOrder.CoreProcessStatus = d.CoreProcessStatus;
                if (d.CoreProcessStatus.HasValue)
                {
                    PurchaseOrder.CoreProcessStatusEnum=((ProcessStatus)d.CoreProcessStatus.Value).ToString();
                }
                PurchaseOrder.SdProcessStatus = d.SdProcessStatus;
                if (d.SdProcessStatus.HasValue)
                {
                    PurchaseOrder.SdProcessStatusEnum = ((ProcessStatus)d.SdProcessStatus.Value).ToString();
                }
                PurchaseOrder.PresalesProcessStatus = d.PresalesProcessStatus;
                if (d.PresalesProcessStatus.HasValue)
                {
                    PurchaseOrder.PresalesProcessStatusEnum = ((ProcessStatus)d.PresalesProcessStatus.Value).ToString();
                }
                PurchaseOrder.VendorProcessStatus = d.VendorProcessStatus;
                if (d.VendorProcessStatus.HasValue)
                {
                    PurchaseOrder.VendorProcessStatusEnum = ((ProcessStatus)d.VendorProcessStatus.Value).ToString();
                }

                PurchaseOrder.PresalesApproval = d.PresalesApproval;
                if (d.PresalesApproval.HasValue)
                {
                    ((Approval)d.PresalesApproval.Value).ToString();
                }
                PurchaseOrder.SDClientVerification = d.SDClientVerification;
                PurchaseOrder.SDSuggestedVendorId1 = d.SDSuggestedVendorId1;
                PurchaseOrder.SDSuggestedVendorId2 = d.SDSuggestedVendorId2;
                PurchaseOrder.SDSuggestedVendorId3 = d.SDSuggestedVendorId3;
                PurchaseOrder.CoreIp = d.CoreIp;
                PurchaseOrder.CoreVlanId_ = d.CoreVlanId_;
                //dAtes
                PurchaseOrder.Date = d.Date;
                PurchaseOrder.UpdatedAt = d.UpdatedAt;
                PurchaseOrder.UpdatedBy = d.UpdatedBy;
                PurchaseOrder.CreatedAt = d.CreatedAt;
                PurchaseOrder.CreatedBy = d.CreatedBy;
               
                response.Data.Add(PurchaseOrder);
            }
            return response;

        }
    }
}