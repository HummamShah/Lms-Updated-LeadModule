using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
	public class GetCompaniesDropdownResponse
	{
		public List<GetCompaniesDropdownData> Data { get; set; }
	}
	public class GetCompaniesDropdownData
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
	public class GetCompaniesDropdownRequest
	{
		public string UserId { get; set; }
		public int LeadId { get; set; }

		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest(GetCompaniesDropdownRequest req)
		{
			var response = new GetCompaniesDropdownResponse();
			response.Data = new List<GetCompaniesDropdownData>();
			var Data = _dbContext.Company.ToList();
			if (req.UserId != null && req.UserId != string.Empty)
			{
				var AgentId = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault().Id;
				if (req.LeadId != 0)
                {
					var AssignedLeadCompanyId = _dbContext.Lead.Where(x => x.Id == req.LeadId).FirstOrDefault().CompanyId;
					Data = Data.Where(x => x.AgentId == AgentId || AssignedLeadCompanyId == x.Id).ToList();
                }
                else
                {
					Data = Data.Where(x => x.AgentId == AgentId).ToList();
				}
				
				
			}
			
			foreach (var d in Data)
			{
				var temp = new GetCompaniesDropdownData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}