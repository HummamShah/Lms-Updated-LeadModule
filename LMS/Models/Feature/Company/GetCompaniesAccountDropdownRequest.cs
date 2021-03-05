using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
 
	public class GetCompaniesAccountDropdownResponse
	{
		public List<GetCompaniesAccountDropdownData> Data { get; set; }
	}
	public class GetCompaniesAccountDropdownData
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public string Name { get; set; }

	}
	public class GetCompaniesAccountDropdownRequest
	{
	

		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest(GetCompaniesAccountDropdownRequest req)
		{
			var response = new GetCompaniesAccountDropdownResponse();
			response.Data = new List<GetCompaniesAccountDropdownData>();
			var Data = _dbContext.Account.ToList();
			
			foreach (var d in Data)
			{
				var temp = new GetCompaniesAccountDropdownData();
				temp.Id = d.Company.Id;
				temp.AccountId = d.Id;
				temp.Name = d.Company.Name;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}