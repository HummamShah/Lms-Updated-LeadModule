using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    public class AddFeasibilityResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class FeasibilityDetails
    {
        public int Id { get; set; }
        public int LeadId {get;set;}
        public string Bandwidth { get; set; }
        public decimal OTC { get; set; }
        public decimal MRC { get; set; }
        public int ConnectivityType { get; set; }
        public int? VendorId { get; set; }
        public string Remarks { get; set; }
    }
    public class AddFeasibilityRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string UserId { get; set; }
        public List<FeasibilityDetails> Feasibility { get; set; }
        public int? Status { get; set; }
        public string PmdRemarks { get; set; } //not binded from angular TODO
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public object RunRequest(AddFeasibilityRequest request)
        {
            var resp = new AddFeasibilityResponse();
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
                var Feasibility = new PmdDetails();
                Feasibility.LeadId = feasibility.LeadId;
                Feasibility.Bandwidth = feasibility.Bandwidth;
                Feasibility.OTC = feasibility.OTC;
                Feasibility.MRC = feasibility.MRC;
                Feasibility.VendorId = feasibility.VendorId;
                Feasibility.Remarks = feasibility.Remarks;
                Feasibility.CreatedAt = DateTime.Now;
                Feasibility.CreatedBy = request.CreatedBy;
                Feasibility.ConnectivityType = feasibility.ConnectivityType;
                _dbContext.PmdDetails.Add(Feasibility);
                var Result = _dbContext.SaveChanges();
            }
            return resp;
        }
    }
}