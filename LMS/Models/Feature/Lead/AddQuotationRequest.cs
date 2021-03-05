using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    
	public class AddQuotationResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddQuotationRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int LeadId { get; set; }
		public decimal? Quotation { get; set; }
		public decimal MRC { get; set; }
		public decimal OTC { get; set; }
		public int QuotationStatus { get; set; }
		public string QuotationRemarks { get; set; }
		public object RunRequest(AddQuotationRequest request)
		{
			var response = new AddQuotationResponse();
			var Lead = _dbContext.Lead.Where(x => x.Id == request.LeadId).FirstOrDefault() ;
			Lead.Quotation = request.Quotation;
			Lead.QuotationMRC = request.MRC;
			Lead.QuotationOTC = request.OTC;
			Lead.QuotationStatus = request.QuotationStatus;
			Lead.QuotationRemarks = request.QuotationRemarks;

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
					Content = "Quoatation has been provided on Lead #" + request.LeadId,
					IsRead = false
				};
				var result=_dbContext.Notification.Add(notification);
            }
			_dbContext.SaveChanges();
			return response;
		}
	}
}