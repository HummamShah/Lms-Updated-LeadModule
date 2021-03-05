using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
   
    public class ChangeLeadStatusResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class ChangeLeadStatusRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int Id { get; set; }
        public int Status { get; set; }

        public object RunRequest(ChangeLeadStatusRequest request)
        {
            var response = new ChangeLeadStatusResponse();
            var Lead = _dbContext.Lead.Where(x => x.Id == request.Id).FirstOrDefault();
            Lead.LeadStatus = (int)LeadStatus.Closed;
            Lead.AssignedPmdId = null;
            Lead.AssignedPreSaleId = null;
            Lead.AssignedToId = null;
            _dbContext.SaveChanges();
            return response;
        }
    }
}