using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
    public class GetTicketCommentsRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public object RunRequest(GetTicketCommentsRequest request)
        {
            var response = new GetTicketCommentsResponse();
            var ticket = _dbContext.Ticket.Where(x => x.Id == request.Id).FirstOrDefault();
            response.TicketComments = new List<TicketComments>();
            foreach (var comment in ticket.TicketComments)
            {
                var cmnt = new TicketComments();
                cmnt.Comment = comment.Comment;
                cmnt.CreatedAt = comment.CreatedAt;
                cmnt.CreatedBy = comment.CreatedBy;
                cmnt.Date = comment.Date;
                cmnt.Id = comment.Id;
                cmnt.TicketId = comment.TicketId;
                response.TicketComments.Add(cmnt);
            }
            return response;
        }
    }
    public class GetTicketCommentsResponse
    {
        public List<TicketComments> TicketComments { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}