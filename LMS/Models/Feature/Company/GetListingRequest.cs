using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
	public class GetListingResponse
	{
		public List<CompanyData> Data { get; set; }
	}
	public class CompanyData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string TaggedAgentName { get; set; }
		public int? TaggedAgentId { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetListingResponse();
			response.Data = new List<CompanyData>();
			var Data = _dbContext.Company;
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