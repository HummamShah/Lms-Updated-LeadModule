using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
    
	public class GetParentCompaniesDropdownResponse
	{
		public List<GetParentCompaniesDropdownData> Data { get; set; }
	}
	public class GetParentCompaniesDropdownData
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
	public class GetParentCompaniesDropdownRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetParentCompaniesDropdownResponse();
			response.Data = new List<GetParentCompaniesDropdownData>();
			var Data = _dbContext.Company;
			foreach (var d in Data)
			{
				var temp = new GetParentCompaniesDropdownData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}