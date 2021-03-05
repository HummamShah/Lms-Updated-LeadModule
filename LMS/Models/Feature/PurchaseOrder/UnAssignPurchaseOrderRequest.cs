using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
   
	public class UnAssignPurchaseOrderResponse
	{

		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class UnAssignPurchaseOrderRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public object RunRequest(UnAssignPurchaseOrderRequest request)
		{
			var response = new UnAssignPurchaseOrderResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.AssignedDepartmentId = null;
			_dbContext.SaveChanges();
			return response;
		}
	}
}