using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
    public class ChangeTicketStatusRequest
    {
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public int Status { get; set; }
		public string RFO { get; set; }
		public object RunRequest(ChangeTicketStatusRequest request)
		{
			var response = new ChangeTicketStatusResponse();
			var Ticket = _dbContext.Ticket.Where(x => x.Id == request.Id).FirstOrDefault();
			Ticket.Status = request.Status;
			if(request.Status == (int)TicketStatus.Resolved)
            {
				Ticket.RestorationDateTime = DateTime.Today;
				Ticket.RFO = request.RFO;
            }
			_dbContext.SaveChanges();
			return response;
		}
	}
    public class ChangeTicketStatusResponse
    {
      
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}