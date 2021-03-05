using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Vendor
{
	public class EditVendorResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
		public class EditVendorRequest
		{
			private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
			public int Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public string UpdatedBy { get; set; }


		public object RunRequest(EditVendorRequest request)
			{
				
				var response = new EditVendorResponse();
				var Vendor = _dbContext.Vendor.FirstOrDefault(x => x.Id == request.Id);	
				Vendor.Description = request.Description;
				Vendor.Name = request.Name;
				Vendor.UpdatedAt = DateTime.Today;
				Vendor.UpdatedBy = request.UpdatedBy;
			_dbContext.SaveChanges();
				return response;
			}
		}
	
}