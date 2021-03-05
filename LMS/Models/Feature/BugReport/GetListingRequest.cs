using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.BugReport
{
    public class GetListingResponse
    {
        public List<BugReportData> Data { get; set; }
    }

    public class BugReportData
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public object RunRequest()
        {
            var response = new GetListingResponse();
            response.Data = new List<BugReportData>();
            var Data = _dbContext.BugReporting;

            foreach (var d in Data)
            {
                var temp = new BugReportData();
                temp.Id = d.Id;
                temp.Type = d.Type;
                temp.Description = d.Description;
                temp.CreatedAt = d.CreatedAt;
                temp.CreatedBy = d.CreatedBy;
                response.Data.Add(temp);
            }
            return response;

            }
        }
}