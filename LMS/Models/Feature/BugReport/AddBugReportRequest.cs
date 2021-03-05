using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.BugReport
{

    public class AddBugRepotResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddBugReportRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int? Type { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set;}
        public DateTime? CreatedAt { get; set; }


        public object RunRequest(AddBugReportRequest request)
        {
            var response = new AddBugRepotResponse();
            var BugReport = new LMS.Models.EntityModel.BugReporting();
            BugReport.Type = request.Type;
            BugReport.Description = request.Description;
            BugReport.CreatedAt = DateTime.Now;
            BugReport.CreatedBy = request.CreatedBy;
            var result = _dbContext.BugReporting.Add(BugReport);
            _dbContext.SaveChanges();
            return response;
        }
    }
}