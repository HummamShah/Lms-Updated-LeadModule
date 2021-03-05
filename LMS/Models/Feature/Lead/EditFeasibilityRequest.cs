using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
   
    public class EditFeasibilityResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }

    public class EditFeasibilityRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string UserId { get; set; }
        public List<FeasibilityDetails> Feasibility { get; set; }
        public List<FeasibilityDetails> DeletedRows { get; set; }
        public int? Status { get; set; }
        public string PmdRemarks { get; set; } //not binded from angular TODO
        public string UpdatedBy { get; set; }
      
        public object RunRequest(EditFeasibilityRequest request)
        {
            var resp = new EditFeasibilityResponse();
            var LeadId = request.Feasibility.FirstOrDefault().LeadId;

            var Lead = _dbContext.Lead.Where(x => x.Id == LeadId).FirstOrDefault();
            Lead.PmdStatus = request.Status;
            if (Lead.PmdStatus == (int)PmdStatus.NotFeasible)
            {
                Lead.LeadStatus = (int)LeadStatus.Cancelled;
            }
            Lead.PmdRemarks = request.PmdRemarks;
            var LeadStatusResult = _dbContext.SaveChanges();
            foreach (var feasibility in request.Feasibility)
            {
                if (feasibility.Id != 0)
                {
                    var Feasibility = _dbContext.PmdDetails.Where(x => x.Id == feasibility.Id).FirstOrDefault();
                    //Feasibility.LeadId = feasibility.LeadId;
                    Feasibility.Bandwidth = feasibility.Bandwidth;
                    Feasibility.OTC = feasibility.OTC;
                    Feasibility.MRC = feasibility.MRC;
                    Feasibility.VendorId = feasibility.VendorId;
                    Feasibility.Remarks = feasibility.Remarks;
                    Feasibility.UpdatedAt = DateTime.Now;
                    Feasibility.UpdatedBy = request.UpdatedBy;
                    Feasibility.ConnectivityType = feasibility.ConnectivityType;
                    var Result = _dbContext.SaveChanges();
                }
                if (feasibility.Id == 0)
                {
                    var Feasibility = new PmdDetails();
                    Feasibility.LeadId = feasibility.LeadId;
                    Feasibility.Bandwidth = feasibility.Bandwidth;
                    Feasibility.OTC = feasibility.OTC;
                    Feasibility.MRC = feasibility.MRC;
                    Feasibility.VendorId = feasibility.VendorId;
                    Feasibility.Remarks = feasibility.Remarks;
                    Feasibility.UpdatedAt = DateTime.Now;
                    Feasibility.UpdatedBy = request.UpdatedBy;
                    Feasibility.ConnectivityType = feasibility.ConnectivityType;
                    _dbContext.PmdDetails.Add(Feasibility);
                    var Result = _dbContext.SaveChanges();
                }
                
            }

           

            foreach (var row in request.DeletedRows )
            {
                if (row.Id != 0)
                {
                    var PmdDetail = _dbContext.PmdDetails.Where(x => x.Id == row.Id).FirstOrDefault();
                    _dbContext.Entry(PmdDetail).State = System.Data.Entity.EntityState.Deleted;
                    _dbContext.SaveChanges();
                }
               
            }
            var AssignedUserContent = "Your Lead " + LeadId + " Has Been Marked " + ((PmdStatus)Lead.LeadStatus.Value).ToString()+" By Pmd ";
            var notification = new LMS.Models.EntityModel.Notification()
            {
                AgentId = Lead.AssignedToId,
                CreatedAt = DateTime.Now,
                Date = DateTime.Now.Date,
                Link = "/Lead/Detail?Id=" + LeadId,
                Content = AssignedUserContent,
                IsRead = false
            };
            var result = _dbContext.Notification.Add(notification);
            _dbContext.SaveChanges();
            if (Lead.AssignedToId != null && Lead.AssignedToId != 0)
            {
                var AuthorContent = "The Lead You Assigned To Agent " + Lead.AssignedTo.FisrtName + " " + Lead.AssignedTo.LastName + " #" + LeadId + " Has Been Marked " + ((PmdStatus)Lead.LeadStatus.Value).ToString() + " By Pmd ";
                var notification2 = new LMS.Models.EntityModel.Notification()
                {
                    AgentId = Lead.AgentId,
                    CreatedAt = DateTime.Now,
                    Date = DateTime.Now.Date,
                    Link = "/Lead/Detail?Id=" + LeadId,
                    Content = AuthorContent,
                    IsRead = false
                };
                var result2 = _dbContext.Notification.Add(notification2);
                _dbContext.SaveChanges();
            }
            else
            {
                var notification2 = new LMS.Models.EntityModel.Notification()
                {
                    AgentId = Lead.AgentId,
                    CreatedAt = DateTime.Now,
                    Date = DateTime.Now.Date,
                    Link = "/Lead/Detail?Id=" + LeadId,
                    Content = AssignedUserContent,
                    IsRead = false
                };
                var result2 = _dbContext.Notification.Add(notification2);
                _dbContext.SaveChanges();
            }
           
            return resp;
        }
    }
}