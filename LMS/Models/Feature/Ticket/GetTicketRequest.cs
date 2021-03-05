using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{

    public class GetTicketResponse
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int? AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public int? AssignedDepartmentId { get; set; }
        public string AssignedDepartmentName { get; set; }
        public int Status { get; set; }
        public string StatusEnum { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public string Region { get; set; }
        public string ComplaintMedia { get; set; }
        public string Remarks { get; set; }
        public string VisitCall { get; set; }
        public DateTime? EscalationDateTime { get; set; }
        public DateTime? RestorationDateTime { get; set; }
        public int? ShiftEngineerAgentId { get; set; }
        public string ShiftEngineerAgentName { get; set; }
        public string RFO { get; set; }
        public string OwnerShip { get; set; }
        public string Problem { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Date { get; set; }
        public List<TicketComments> TicketComments { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class TicketComments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Date { get; set; }

    }
    public class GetTicketRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
  
        public object RunRequest(GetTicketRequest request)
        {
            var response = new GetTicketResponse();
            var ticket = _dbContext.Ticket.Where(x => x.Id == request.Id).FirstOrDefault();
            response.Id = ticket.Id;
            response.AccountId = ticket.AccountId;
            response.AgentId = ticket.AgentId;
            response.AssignedDepartmentId = ticket.AssignedDepartmentId;
            response.AssignedToId = ticket.AssignedToId;
            if (ticket.AssignedToId.HasValue)
            {
                response.AssignedToName = ticket.AssignedAgent.FisrtName + " " + ticket.AssignedAgent.LastName;
            }
            response.ComplaintMedia = ticket.ComplaintMedia;
            response.EscalationDateTime = ticket.EscalationDateTime;
            response.Media = ticket.Media;
            response.Name = ticket.Name;
            response.OwnerShip = ticket.OwnerShip;
            response.Region = ticket.Region;
            response.Remarks = ticket.Remarks;
            response.RestorationDateTime = ticket.RestorationDateTime;
            response.RFO = ticket.RFO;
            response.CreatedAt = ticket.CreatedAt;
            response.CreatedBy = ticket.CreatedBy;
            response.ShiftEngineerAgentId = ticket.ShiftEngineerAgentId;
            if (ticket.ShiftEngineerAgentId.HasValue)
            {
                response.ShiftEngineerAgentName = ticket.ShiftEngineerAgent.FisrtName + " " + ticket.ShiftEngineerAgent.LastName;
            }
            response.Status = ticket.Status;
            response.StatusEnum = ((TicketStatus)ticket.Status).ToString();
            if (ticket.VendorId.HasValue)
            {
                response.VendorId = ticket.VendorId;
                response.VendorName = ticket.Vendor.Name;
            }
            response.VisitCall = ticket.VisitCall;
            response.Problem = ticket.Problem;
            response.UpdatedAt = ticket.UpdatedAt;
            response.UpdatedBy = ticket.UpdatedBy;
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
}