using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
 
	public class ChangePurchaseOrderStatusResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class ChangePurchaseOrderStatusRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int Status { get; set; }
		public object RunRequest(ChangePurchaseOrderStatusRequest request)
		{
			var response = new ChangePurchaseOrderStatusResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.Status = request.Status;
			_dbContext.SaveChanges();
			return response;
		}
	}
}