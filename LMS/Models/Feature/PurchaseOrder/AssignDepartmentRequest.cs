using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.PurchaseOrder
{
   
	public class AssignDepartmentResponse
	{
		public bool IsDepartmentAssigned { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AssignDepartmentRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int DepartmentId { get; set; }


		public object RunRequest(AssignDepartmentRequest request)
		{
			var response = new AssignDepartmentResponse();
			var PurchaseOrder = _dbContext.PurchaseOrder.Where(x => x.Id == request.Id).FirstOrDefault();
			PurchaseOrder.AssignedDepartmentId = request.DepartmentId;
			_dbContext.SaveChanges();
			return response;
		}
	}
}