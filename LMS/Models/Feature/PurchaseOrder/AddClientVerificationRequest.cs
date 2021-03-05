using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
    
	public class AddClientVerificationResponse
	{
		public bool IsPOApproved { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddClientVerificationRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public bool? SDClientVerification { get; set; }
		public object RunRequest(AddClientVerificationRequest request)
		{
			var response = new AddClientVerificationResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.SDClientVerification = request.SDClientVerification;
			_dbContext.SaveChanges();
			return response;
		}
	}
}