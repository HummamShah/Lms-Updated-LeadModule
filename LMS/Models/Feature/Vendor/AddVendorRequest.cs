using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace LMS.Models.Feature.Vendor
{
   
	public class AddVendorResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddVendorRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
	
		public object RunRequest(AddVendorRequest request)
		{
			var response = new AddVendorResponse();
			var Vendor = new LMS.Models.EntityModel.Vendor();
			Vendor.CreatedAt = DateTime.Today;
			Vendor.CreatedBy = request.CreatedBy;
			Vendor.Description = request.Description;
			Vendor.Name = request.Name;
			var result = _dbContext.Vendor.Add(Vendor);
			_dbContext.SaveChanges();
			return response;
		}
	}
}