using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Department
{
    
	public class AddDepartmentResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddDepartmentRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }

		public object RunRequest(AddDepartmentRequest request)
		{
			var response = new AddDepartmentResponse();
			var Department = new LMS.Models.EntityModel.Department();
			Department.CreatedAt = DateTime.Today;
			Department.CreatedBy = request.CreatedBy;
			Department.Description = request.Description;
			Department.Name = request.Name;
			var result = _dbContext.Department.Add(Department);
			_dbContext.SaveChanges();
			return response;
		}
	}
}