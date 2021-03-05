using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Department
{
 
	public class GetDepartmentsDropdownResponse
	{
		public List<DepartmentDropDownData> Data { get; set; }
	}
	public class DepartmentDropDownData
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
	public class GetDepartmentsDropdownRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetDepartmentsDropdownResponse();
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
				//Sales_Lead,PMD,Pre_Sales,Closer,Administration
				new DepartmentDropDownData(){ Id=1,Name="Sales_Lead"},
				new DepartmentDropDownData(){ Id=2,Name="PMD"},
				new DepartmentDropDownData(){ Id=3,Name="Pre_Sales"},
				//new DepartmentDropDownData(){ Id=4,Name="Closer"},
				new DepartmentDropDownData(){ Id=5,Name="Administration"},
				new DepartmentDropDownData(){ Id=6,Name="Accounts"},
				new DepartmentDropDownData(){ Id=7,Name="Core"},
				new DepartmentDropDownData(){ Id=8,Name="Service Desk"},
				new DepartmentDropDownData(){ Id=9,Name="HR"},
			};


			response.Data = Data;
			return response;
		}
	}
}