using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
	public class GetBranchesListingResponse
	{
		public List<CompanyData> Data { get; set; }
	}
	public class GetBranchesListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		//Pagination Fields future task
		public int Type { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int CompanyId { get; set; }
		public object RunRequest(GetBranchesListingRequest req)
		{
			var response = new GetBranchesListingResponse();
			response.Data = new List<CompanyData>();
			var Data = _dbContext.Company.Where(x => x.ParentCompanyId == req.CompanyId && x.IsBranch == true);
			foreach (var d in Data)
			{
				var temp = new CompanyData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				temp.Description = d.Description;
				temp.Address = d.Address;
				temp.Contact = d.Contact;
				temp.Email = d.Email;
				temp.CreatedAt = d.CreatedAt;
				temp.CreatedBy = d.CreatedBy;
				temp.TaggedAgentId = d.AgentId;
				if (d.AgentId != null)
				{
					temp.TaggedAgentName = d.Agent.FisrtName + " " + d.Agent.LastName;
				}

				response.Data.Add(temp);
			}
			return response;


		}
	}
}