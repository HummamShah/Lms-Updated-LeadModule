using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.User
{
	public class GetListingResponse
	{
		public List<UserData> Data { get; set; }
	}
	public class UserData
	{
	//	[LastName] nvarchar(max),
	//[Contact1] nvarchar(max),
	//[Contact2] nvarchar(max),
	//[Address] nvarchar(max),
	//[Designation] int,
	//[DepartmentId] int,
	//[Email] nvarchar(max),
	//[ImageUrl] nvarchar(max),
	//[IsActive] bit,
		public int Id { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string DesignationEnum { get; set; }
		public int? Designation { get; set; }
		public int? DepartmentId { get; set; }
		public string  DepartmentName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetListingResponse();
			response.Data = new List<UserData>();
			var Data = _dbContext.Agent;
			foreach (var d in Data)
			{
				var temp = new UserData();
				temp.Id = d.Id;
				temp.FirstName = d.FisrtName;
				temp.LastName = d.LastName;
				temp.UserId = d.UserId;
				temp.Contact1 = d.Contact1;
				temp.Contact2 = d.Contact2;
				temp.Address = d.Address;
				temp.Email = d.Email;
				temp.CreatedAt = d.CreatedAt;
				temp.CreatedBy = d.CreatedBy;
				temp.DepartmentId = d.DepartmentId;
				temp.Designation = d.Designation;
                if (d.DepartmentId.HasValue)
                {
					temp.DepartmentName = ((Departments)d.DepartmentId.Value).ToString();

				}
                if (d.Designation.HasValue)
                {
					temp.DesignationEnum = ((Designation)d.Designation.Value).ToString();
				}
				response.Data.Add(temp);
			}
			return response;
		}
	}
}