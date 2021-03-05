using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Department
{
	public class GetDepartmentRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public object RunRequest(GetDepartmentRequest req)
		{
			var response = new GetDepartmentResponse();
			var Data = _dbContext.Department.Where(x => x.Id == req.Id).FirstOrDefault();
			response.Id = Data.Id;
			response.Name = Data.Name;
			response.Description = Data.Description;
			response.CreatedAt = Data.CreatedAt;
			response.CreatedBy = Data.CreatedBy;
			response.UpdatedAt = Data.UpdatedAt;
			response.UpdatedBy = Data.UpdatedBy;
			return response;
		}
	}
	public class GetDepartmentResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

	}
}