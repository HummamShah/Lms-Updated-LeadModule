using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Ticket
{
    public class GetListingResponse
    {
        public List<TicketData> Data { get; set; }
    }

    public class TicketData
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
        public string Problem { get; set; }
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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Date { get; set; }
        // public List<TicketDetails> TicketDetails { get; set; }


    }
    //public class TicketDetails
    //{

    //}

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<TicketData>();
            var Data = _dbContext.Ticket;

            foreach (var d in Data)
            {
                var Ticket = new TicketData();
                Ticket.Id = d.Id;
                Ticket.Name = d.Name;
                Ticket.AccountId = d.AccountId;
                Ticket.AccountName = d.Account.Name;
                Ticket.AgentId = d.AgentId;
                Ticket.AgentName = d.Agent.FisrtName + " " + d.Agent.LastName;
                Ticket.AssignedDepartmentId = d.AssignedDepartmentId;
                if (d.AssignedDepartmentId.HasValue)
                {
                    Ticket.AssignedDepartmentName = ((Departments)d.AssignedDepartmentId.Value).ToString();
                }
                Ticket.AssignedToId = d.AssignedToId;
                if (d.AssignedToId.HasValue)
                {
                    Ticket.AssignedToName = d.AssignedAgent.FisrtName = " " + d.AssignedAgent.LastName;
                }
                Ticket.ComplaintMedia = d.ComplaintMedia;
                Ticket.EscalationDateTime = d.EscalationDateTime;
                Ticket.Media = d.Media;
                Ticket.OwnerShip = d.OwnerShip;
                Ticket.Region = d.Region;
                Ticket.Remarks = d.Remarks;
                Ticket.RestorationDateTime = d.RestorationDateTime;
                Ticket.RFO = d.RFO;
                Ticket.ShiftEngineerAgentId = d.ShiftEngineerAgentId;
                if (d.ShiftEngineerAgentId.HasValue)
                {
                    Ticket.ShiftEngineerAgentName = d.ShiftEngineerAgent.FisrtName + " " + d.ShiftEngineerAgent.LastName;
                }
                Ticket.Status = d.Status;
                Ticket.StatusEnum = ((TicketStatus)d.Status).ToString();
                if (d.VendorId.HasValue)
                {
                    Ticket.VendorId = d.VendorId;
                    Ticket.VendorName = d.Vendor.Name;
                }
                Ticket.VisitCall = d.VisitCall;
                Ticket.Problem = d.Problem;
                Ticket.Date = d.Date;
                Ticket.UpdatedAt = d.UpdatedAt;
                Ticket.UpdatedBy = d.UpdatedBy;
                Ticket.CreatedAt = d.CreatedAt;
                Ticket.CreatedBy = d.CreatedBy;
                response.Data.Add(Ticket);
            }
            return response;

        }
    }
}