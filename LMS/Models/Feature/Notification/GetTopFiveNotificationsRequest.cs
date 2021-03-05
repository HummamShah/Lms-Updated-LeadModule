using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Notification
{
   
    public class GetTopFiveNotificationsResponse
    {
        public List<NotificationData> Data { get; set; }
        public int TotalNotification { get; set; }
    }
   
    public class GetTopFiveNotificationsRequest
    {
        public string UserId { get; set; }

        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public object RunRequest(GetTopFiveNotificationsRequest req)
        {
            var Agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
            var response = new GetTopFiveNotificationsResponse();
            response.Data = new List<NotificationData>();
            var AgentId = Agent.Id;
            var Data = _dbContext.Notification.Where(x => x.AgentId == AgentId).OrderByDescending(x=>x.Id).ToList();
            response.TotalNotification = Data.Count();
            Data = Data.Take(10).ToList();
            foreach (var d in Data)
            {
                var notification = new NotificationData();
                notification.Id = d.Id;
                notification.Date = d.Date;
                notification.Content = d.Content;
                notification.Link = d.Link;
                notification.IsRead = d.IsRead;
                notification.AgentId = d.AgentId.Value;
                notification.AgentName = d.Agent.FisrtName + " " + d.Agent.LastName;
                notification.CreatedBy = d.CreatedBy;
                response.Data.Add(notification);

            }
            return response;
        }
    }
}