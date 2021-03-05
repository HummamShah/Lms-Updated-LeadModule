using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
    public class EditTicketCommentsRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public string UpdatedBy { get; set; }
        public string Comment { get; set; }
        public List<TicketComments> DeletedComments { get; set; }
        public object RunRequest(EditTicketCommentsRequest request)
        {
            var response = new EditTicketCommentsResponse();
            var TicketComment = _dbContext.TicketComments.Where(x => x.Id == request.Id).FirstOrDefault();
            TicketComment.Comment = request.Comment;
            TicketComment.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
            foreach (var comment in request.DeletedComments)
            {
                if (comment.Id != 0)
                {
                    var TicketDetail = _dbContext.TicketComments.Where(x => x.Id == comment.Id).FirstOrDefault();
                    _dbContext.Entry(TicketDetail).State = System.Data.Entity.EntityState.Deleted;
                    _dbContext.SaveChanges();
                }
            }
            return response;
        }

    }
    public class EditTicketCommentsResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}