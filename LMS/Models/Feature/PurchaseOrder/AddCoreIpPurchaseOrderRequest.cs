using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
    
	public class AddCoreIpPurchaseOrderResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddCoreIpPurchaseOrderRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public string CoreIp { get; set; }
		public string CoreVlanId_ { get; set; }



		public object RunRequest(AddCoreIpPurchaseOrderRequest request)
		{
			var response = new AddCoreIpPurchaseOrderResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.CoreIp = request.CoreIp;
			PurchaseOrder.CoreVlanId_ = request.CoreVlanId_;
			_dbContext.SaveChanges();
			return response;
		}
	}
}