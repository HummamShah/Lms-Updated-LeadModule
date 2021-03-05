using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Demo
{
    public class DemoResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
    public class DemoRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public object RunRequest()
        {
            var resp = new DemoResponse();
            var user = _dbContext.AspNetUsers.FirstOrDefault();
            resp.Id = user.Id;
            resp.Email = user.Email;
            return resp;
        }
    }
}