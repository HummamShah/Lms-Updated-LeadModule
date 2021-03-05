using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanyAccounts
{
   
	public class GetCompanyAccountsDropdownResponse
	{
		public List<GetCompaniesDropdownData> Data { get; set; }
	}
	public class GetCompaniesDropdownData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AccountId { get; set; }

	}
	public class GetCompanyAccountsDropdownRequest
	{
		//public string UserId { get; set; }
		//public int LeadId { get; set; }

		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest(GetCompanyAccountsDropdownRequest req)
		{
			var response = new GetCompanyAccountsDropdownResponse();
			response.Data = new List<GetCompaniesDropdownData>();
			var Data = _dbContext.Account.ToList();
			foreach (var d in Data)
			{
				var temp = new GetCompaniesDropdownData();
				temp.Id = d.CompanyId;
				temp.Name = d.Company.Name;
				temp.AccountId = d.Id;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}