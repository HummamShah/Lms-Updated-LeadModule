using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Vendor
{

	public class GetVendorsResponse
	{
		public List<VendorsData> Data { get; set; }
	}
	public class VendorsData
	{
		public int Id { get; set; }
		public string Name { get; set; }



	}
	public class GetVendorsRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public object RunRequest()
		{
			var response = new GetVendorsResponse();
			response.Data = new List<VendorsData>();
			var Data = _dbContext.Vendor;
			foreach (var d in Data)
			{
				var temp = new VendorsData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}