using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    public class EditLeadStatusResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }

    public class EditLeadStatusRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int LeadId { get; set; }
        public bool? isApproved { get; set; }

        public object RunRequest (EditLeadStatusRequest request)
        {
            var response = new EditLeadStatusResponse();
            var Lead = _dbContext.Lead.Where(x => x.Id == request.LeadId).FirstOrDefault();
            var Content="";
            if(Lead.IsApproved==true)
            {
                Lead.LeadStatus = (int)LeadStatus.Completed;
                Content="Lead #"+request.LeadId +"has been Completed and Closed";
            }
            //if(Lead.IsApproved == false)
            //{
            //    Content = "Lead #" + request.LeadId + "has been Cancled and Closed";
            //}
            var Admins = from user in _dbContext.AspNetUsers
                         where user.AspNetRoles.Any(r => r.Name == Roles.Admin.ToString())
                         select user;
            foreach (var admin in Admins)
            {
                var notification = new LMS.Models.EntityModel.Notification()
                {
                    AgentId = admin.Agent.FirstOrDefault().Id,
                    CreatedAt = DateTime.Now,
                    Date = DateTime.Now.Date,
                    Link = "/Lead/Detail?Id=" + request.LeadId,
                    Content = Content,
                    IsRead = false
                };
                var result = _dbContext.Notification.Add(notification);
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
            if (Lead.AssignedToId != null)
            {
                var AssignedSalesNotification = new LMS.Models.EntityModel.Notification();
                AssignedSalesNotification.AgentId = Lead.AgentId;
                AssignedSalesNotification.Link = "/Lead/Detail?Id=" + request.LeadId;
                AssignedSalesNotification.CreatedAt = DateTime.Now;
                AssignedSalesNotification.Date = DateTime.Now.Date;
                AssignedSalesNotification.Content = Content;
                var result = _dbContext.Notification.Add(AssignedSalesNotification);
            }
            _dbContext.SaveChanges();
            return response;
        }
    }
}