using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
 
	public class TagAgentResponse
	{
		public bool IsUserTagged { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class TagAgentRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int CompanyId { get; set; }
		public int AgentId { get; set; }


		public object RunRequest(TagAgentRequest request)
		{
			var response = new TagAgentResponse();
			var Notification = new LMS.Models.EntityModel.Notification();
			var Company = _dbContext.Company.Where(x => x.Id == request.CompanyId).FirstOrDefault();
			Company.AgentId = request.AgentId;
			Notification.AgentId = request.AgentId ;
			Notification.CreatedAt = DateTime.Now;
			Notification.Date = DateTime.Now.Date;
			Notification.Content = "Company # " + request.CompanyId + " Has been tagged to you!";
			Notification.Link = "/Company/Detail?Id=" + request.CompanyId;
			Notification.IsRead = false;
			var result = _dbContext.Notification.Add(Notification);
			_dbContext.SaveChanges();
			return response;
		}
	}
}