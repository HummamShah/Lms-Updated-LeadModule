using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
   
	public class ChangePurchaseOrderBillingStatusResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class ChangePurchaseOrderBillingStatusRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int BillingStatus { get; set; }
		public object RunRequest(ChangePurchaseOrderBillingStatusRequest request)
		{
			var response = new ChangePurchaseOrderBillingStatusResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.BillingStatus = request.BillingStatus;
			_dbContext.SaveChanges();
			return response;
		}
	}
}