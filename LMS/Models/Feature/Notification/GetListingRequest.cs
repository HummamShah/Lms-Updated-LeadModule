using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Notification
{
    public class GetListingResponse
    {
        public List<NotificationData> Data { get; set; }
        public int TotalRecords { get; set; }
    }
    public class NotificationData
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public string Link { get; set; }
        public DateTime? Date { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string CreatedBy { get; set; }
    }
    public class GetListingRequest
    {
        public string UserId { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public object RunRequest(GetListingRequest req)
        {
            //var SkippingNumber = req.PageSize * (req.CurrentPage - 1);
            var Agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
            var response = new GetListingResponse();
            response.Data = new List<NotificationData>();
            var AgentId = Agent.Id;
            var Data = _dbContext.Notification.Where(x=>x.AgentId==AgentId).ToList();
            //response.TotalRecords = Data.Count();
            //Data = Data.Skip(SkippingNumber).Take(req.PageSize).ToList();
            foreach (var d in Data)
            {
                var notification = new NotificationData();
                notification.Id = d.Id;
                notification.Date = d.Date;
                notification.Content = d.Content;
                notification.Link = d.Link;
                notification.IsRead = d.IsRead;
                notification.AgentId = d.AgentId.Value;
                notification.AgentName = d.Agent.FisrtName + " " +  d.Agent.LastName;
                notification.CreatedBy = d.CreatedBy;
                response.Data.Add(notification);

            }
            return response;
        }
    }
}