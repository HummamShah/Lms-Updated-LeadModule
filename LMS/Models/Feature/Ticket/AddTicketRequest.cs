using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
   
    public class AddTicketResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddTicketRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int AgentId { get; set; }
        public string UserId { get; set; }
        public int AccountId { get; set; }
        public int? AssignedToId { get; set; }
        public int? AssignedDepartmentId { get; set; }
        public string Problem { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public int? VendorId { get; set; }
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
        public string CreatedBy { get; set; }
        public object RunRequest(AddTicketRequest request)
        {
            var response = new AddTicketResponse();
            var AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
            var ticket = new LMS.Models.EntityModel.Ticket();
            ticket.AccountId = request.AccountId;
            ticket.AgentId = AgentId;
            ticket.AssignedDepartmentId = request.AssignedDepartmentId;
            ticket.AssignedToId = request.AssignedToId;
            ticket.ComplaintMedia = request.ComplaintMedia;
            ticket.EscalationDateTime = DateTime.Now;
            ticket.Media = request.Media;
            ticket.Name = request.Name;
            ticket.OwnerShip = request.OwnerShip;
            ticket.Region = request.Region;
            ticket.Remarks = request.Remarks;
            //ticket.RestorationDateTime = request.RestorationDateTime;
            ticket.RFO = request.RFO;
            ticket.ShiftEngineerAgentId = request.ShiftEngineerAgentId;
            ticket.Status = (int)TicketStatus.Open;
            ticket.VendorId = request.VendorId;
            ticket.VisitCall = request.VisitCall;
            ticket.Problem = request.Problem;
            ticket.CreatedAt = DateTime.Now;
            ticket.CreatedBy = request.CreatedBy;
            var result = _dbContext.Ticket.Add(ticket);
            _dbContext.SaveChanges();
            return response;
        }
    }
}