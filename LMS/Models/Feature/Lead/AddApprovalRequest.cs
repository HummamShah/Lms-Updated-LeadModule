using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{

    public class AddApprovalResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddApprovalRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int LeadId { get; set; }
        public int AssignedPreSaleId { get;set; }
        public bool? IsApproved { get; set; }
        public string AdminRemarks { get; set; }


        public object RunRequest(AddApprovalRequest request)
        {

            var response = new AddApprovalResponse();
            var Lead = _dbContext.Lead.Where(x => x.Id == request.LeadId).FirstOrDefault();
            
            Lead.IsApproved = request.IsApproved;
            Lead.AdminRemarks = request.AdminRemarks;
            _dbContext.SaveChanges();
            if (request.IsApproved != null)
            {
                var Content = "";
                if(Lead.IsApproved == false)
                {
                     Lead.LeadStatus = (int)LeadStatus.Cancelled;
                     Content = "Lead #" + request.LeadId + " Has been Disapproved";
                }
                if (Lead.IsApproved == true)
                {
                    Content = "Lead #" + request.LeadId + " Has been Approved";
                }

            
            
                if (Lead.AssignedPmdId != null)
                {
                    var PMDNotification = new LMS.Models.EntityModel.Notification();
                    PMDNotification.AgentId = Lead.AssignedPmdId;
                    PMDNotification.Link = "/Lead/Detail?Id=" + request.LeadId;
                    PMDNotification.CreatedAt = DateTime.Now;
                    PMDNotification.Date = DateTime.Now.Date;
                    PMDNotification.Content = Content;
                    var result = _dbContext.Notification.Add(PMDNotification);
                }
                if (Lead.AssignedPreSaleId != null)
                {
                    var PresalesNotification = new LMS.Models.EntityModel.Notification();
                    PresalesNotification.AgentId = Lead.AssignedPreSaleId;
                    PresalesNotification.Link = "/Lead/Detail?Id=" + request.LeadId;
                    PresalesNotification.CreatedAt = DateTime.Now;
                    PresalesNotification.Date = DateTime.Now.Date;
                    PresalesNotification.Content = Content;
                    var result = _dbContext.Notification.Add(PresalesNotification);
                }
                if (Lead.AgentId != null)
                {
                    var SalesNotification = new LMS.Models.EntityModel.Notification();
                    SalesNotification.AgentId = Lead.AgentId;
                    SalesNotification.Link = "/Lead/Detail?Id=" + request.LeadId;
                    SalesNotification.CreatedAt = DateTime.Now;
                    SalesNotification.Date = DateTime.Now.Date;
                    SalesNotification.Content = Content;
                    var result = _dbContext.Notification.Add(SalesNotification);
                }
                if (Lead.AssignedToId != null)
                {
                    var AssignedSalesNotification = new LMS.Models.EntityModel.Notification();
                    AssignedSalesNotification.AgentId = Lead.AssignedToId;
                    AssignedSalesNotification.Link = "/Lead/Detail?Id=" + request.LeadId;
                    AssignedSalesNotification.CreatedAt = DateTime.Now;
                    AssignedSalesNotification.Date = DateTime.Now.Date;
                    AssignedSalesNotification.Content = Content;
                    var result = _dbContext.Notification.Add(AssignedSalesNotification);
                }
                _dbContext.SaveChanges();
            }
            return response;
        }
    }
}