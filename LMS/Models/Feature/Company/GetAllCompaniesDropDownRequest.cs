using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
    
	public class GetAllCompaniesDropDownResponse
	{
		public List<GetCompaniesDropdownData> Data { get; set; }
	}
	
	public class GetAllCompaniesDropDownRequest
	{

		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetAllCompaniesDropDownResponse();
			response.Data = new List<GetCompaniesDropdownData>();
			var Data = _dbContext.Company.ToList();
			

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