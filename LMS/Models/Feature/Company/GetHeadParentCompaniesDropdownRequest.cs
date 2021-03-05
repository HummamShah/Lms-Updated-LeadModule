using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{

	public class GetHeadParentCompaniesDropdownResponse
	{
		public List<GetHeadParentCompaniesDropdownData> Data { get; set; }
	}
	public class GetHeadParentCompaniesDropdownData
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
	public class GetHeadParentCompaniesDropdownRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetHeadParentCompaniesDropdownResponse();
			response.Data = new List<GetHeadParentCompaniesDropdownData>();
			var Data = _dbContext.Company.Where(x=>x.IsParent == true);
			foreach (var d in Data)
			{
				var temp = new GetHeadParentCompaniesDropdownData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}