using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
 
    public class GetLeadByIdResponse
    {
        public bool IsLeadPresent { get; set; }
        public int CompanyId { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class GetLeadByIdRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int Id { get; set; }

        public object RunRequest(GetLeadByIdRequest request)
        {

            var response = new GetLeadByIdResponse();
            var Lead = _dbContext.Lead.Where(x => x.Id == request.Id).FirstOrDefault();
            if (Lead != null)
            {
                response.CompanyId = Lead.CompanyId.Value;
                response.IsLeadPresent = true;
            }
          
            return response;
        }
    }
}