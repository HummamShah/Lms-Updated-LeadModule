using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Vendor
{

	public class GetListingResponse
	{
		public List<VendorData> Data { get; set; }
	}
	public class VendorData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetListingResponse();
			response.Data = new List<VendorData>();
			var Data = _dbContext.Vendor;
			foreach (var d in Data)
			{
				var temp = new VendorData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				temp.Description = d.Description;
				temp.CreatedAt = d.CreatedAt;
				temp.CreatedBy = d.CreatedBy;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}