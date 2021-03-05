using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    public class ChangeFeasibilityStatusRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int Id { get; set; }
        public int Status { get; set; }

        public object RunRequest(ChangeFeasibilityStatusRequest request)
        {
            var response = new ChangeLeadStatusResponse();
            var Lead = _dbContext.Lead.Where(x => x.Id == request.Id).FirstOrDefault();
            Lead.PmdStatus = request.Status;
            _dbContext.SaveChanges();
            return response;
        }
    }
    public class ChangeFeasibilityStatusResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}