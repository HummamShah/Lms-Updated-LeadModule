using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
  
	public class ApprovePurchaseOrderResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class ApprovePurchaseOrderRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int ApprovalId { get; set; }


		public object RunRequest(ApprovePurchaseOrderRequest request)
		{
			var response = new ApprovePurchaseOrderResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.PresalesApproval = request.ApprovalId;
            if (request.ApprovalId == (int) Approval.NotApproved)
            {
				PurchaseOrder.Status = (int)PurchaseOrderStatus.Cancelled;
            }
			_dbContext.SaveChanges();
			return response;
		}
	}
}