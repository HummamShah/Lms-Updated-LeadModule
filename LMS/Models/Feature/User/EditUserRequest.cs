using LMS.Models.EntityModel;
using LMS.Models.Enums;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LMS.Models.Feature.User
{
   
    public class EditUserResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool IsRoleAdded { get; set; }
        public bool Success { get; set; }
    }
   
    
    public class EditUserRequest
    {
  
        public int AgentId { get; set; }
        public string UpdatedBy { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Email { get; set; }
        public bool? HasSupervisor { get; set; }
        public int? SupervisorId { get; set; }

       
    }
}