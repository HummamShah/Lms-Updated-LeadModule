using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
    public class AddTicketCommentsResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddTicketCommentsRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
       
        public int TicketId { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public object RunRequest(AddTicketCommentsRequest request)
        {
            var response = new AddTicketCommentsResponse();
            var TicketComment = new LMS.Models.EntityModel.TicketComments
            {
                Comment = request.Comment,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.Now,
                Date = DateTime.Now.Date,
                TicketId = request.TicketId

            };
            _dbContext.TicketComments.Add(TicketComment);
            var Ticket = _dbContext.Ticket.Where(x => x.Id == request.TicketId).FirstOrDefault();
            if (Ticket.Status != (int)TicketStatus.FollowUp)
            {
                Ticket.Status = (int)TicketStatus.FollowUp;
            }
            
            _dbContext.SaveChanges();
            return response;
        }
    }
}