using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Controllers.Api
{
    
    public class AddAgentAttendanceResponse
    {
        
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddAgentAttendanceRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string UserId { get; set; }
        public object RunRequest(AddAgentAttendanceRequest request)
        {
            var response = new AddAgentAttendanceResponse();
            var Attendance = new LMS.Models.EntityModel.AgentAttendance();
            Attendance.AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).First().Id;
            Attendance.CreatedAt = DateTime.Now;
            Attendance.Date = DateTime.Now.Date;
            Attendance.IsPresent = true;
            var result = _dbContext.AgentAttendance.Add(Attendance);
            _dbContext.SaveChanges();
            return response;
        }
    }
}