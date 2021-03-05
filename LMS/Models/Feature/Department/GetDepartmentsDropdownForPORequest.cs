using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Department
{
  
	public class GetDepartmentsDropdownForPOResponse
	{
		public List<DepartmentDropDownData> Data { get; set; }
	}
	//public class DepartmentDropDownData
	//{
	//	public int Id { get; set; }
	//	public string Name { get; set; }

	//}
	public class GetDepartmentsDropdownForPORequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetDepartmentsDropdownForPOResponse();
			response.Data = new List<DepartmentDropDownData>();
			//var Data = _dbContext.Department.Where(x=>x.Name != Departments.Administration.ToString());
			//foreach (var d in Data)
			//{
			//	var temp = new DepartmentDropDownData();
			//	temp.Id = d.Id;
			//	temp.Name = d.Name;
			//	response.Data.Add(temp);
			//}
			var Data = new List<DepartmentDropDownData>()
			{
				new DepartmentDropDownData(){ Id=3,Name="Pre_Sales"},
				new DepartmentDropDownData(){ Id=6,Name="Accounts"},
				new DepartmentDropDownData(){ Id=7,Name="Core"},
				new DepartmentDropDownData(){ Id=8,Name="Service Desk"},
			};


			response.Data = Data;
			return response;
		}
	}
}