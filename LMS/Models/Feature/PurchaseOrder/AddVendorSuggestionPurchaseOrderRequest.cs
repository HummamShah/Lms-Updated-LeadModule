using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{

	public class AddVendorSuggestionPurchaseOrderResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddVendorSuggestionPurchaseOrderRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int? SDSuggestedVendorId1 { get; set; }
		public int? SDSuggestedVendorId2 { get; set; }
		public int? SDSuggestedVendorId3 { get; set; }



		public object RunRequest(AddVendorSuggestionPurchaseOrderRequest request)
		{
			var response = new AddVendorSuggestionPurchaseOrderResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.SDSuggestedVendorId1 = request.SDSuggestedVendorId1;
			PurchaseOrder.SDSuggestedVendorId2 = request.SDSuggestedVendorId2;
			PurchaseOrder.SDSuggestedVendorId3 = request.SDSuggestedVendorId3;
			_dbContext.SaveChanges();
			return response;
		}
	}
}