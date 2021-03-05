using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.AgentAttendance
{
	public class GetListingResponse
	{
		public List<AgentAttendanceData> Data { get; set; }
	}
	public class AgentAttendanceData
	{
		public int Id { get; set; }
		public int AgentId { get; set; }
		public string AgentName { get; set; }
		public DateTime? StartDateTime { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDateTime { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? Date { get; set; }
		public bool IsPresent { get; set; }
		public bool IsLate { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public DateTime  Date { get; set; }
		//Pagination Fields
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public object RunRequest(GetListingRequest request)
		{
			var response = new GetListingResponse();
			response.Data = new List<AgentAttendanceData>();
			//var Companies = _dbContext.Company.Where(x=>x.BillingInformationId != null);
			var Attendances = _dbContext.AgentAttendance.Where(x=>x.Date == request.Date);
			foreach (var attendance in Attendances)
			{
				var AgentAttendance = new AgentAttendanceData();
				AgentAttendance.Id = attendance.Id;
				AgentAttendance.AgentId = attendance.AgentId;
				AgentAttendance.AgentName = attendance.Agent.FisrtName + " " + attendance.Agent.LastName;
				AgentAttendance.CreatedAt = attendance.CreatedAt;
				AgentAttendance.CreatedBy = attendance.CreatedBy;
				AgentAttendance.Date = attendance.Date;
				AgentAttendance.EndDate = attendance.EndDate;
				AgentAttendance.EndDateTime = attendance.EndDateTime;
				AgentAttendance.IsLate = attendance.IsLate;
				AgentAttendance.IsPresent = attendance.IsPresent;
				AgentAttendance.StartDate = attendance.StartDate;
				AgentAttendance.StartDateTime = attendance.StartDateTime;
				AgentAttendance.UpdatedAt = attendance.UpdatedAt;
				AgentAttendance.UpdatedBy = attendance.UpdatedBy;
				response.Data.Add(AgentAttendance);
			}
			return response;
		}
	}
}