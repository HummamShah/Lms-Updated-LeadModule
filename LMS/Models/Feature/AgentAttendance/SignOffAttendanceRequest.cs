using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.AgentAttendance
{
    public class SignOffAttendanceResponse 
    {
        public List<string> ValidationErrors { get; set; }
    }
    public class SignOffAttendanceRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string UserId { get; set; }
        public object RunRequest(SignOffAttendanceRequest request)
        {
            var response = new SignOffAttendanceResponse();
            var today = DateTime.Now.Date;
            var Agent = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault();

            if (Agent != null)
            {
                int AgentId = Agent.Id;
                var TodaysAgentAttendance = _dbContext.AgentAttendance.Where(x => x.Date == today && x.AgentId == AgentId).FirstOrDefault();
                if (TodaysAgentAttendance != null)
                {
                    TodaysAgentAttendance.EndDate = DateTime.Now.Date;
                    TodaysAgentAttendance.EndDateTime = DateTime.Now;
                    _dbContext.SaveChanges();
                }
            }
            return response;
        }
    }
}