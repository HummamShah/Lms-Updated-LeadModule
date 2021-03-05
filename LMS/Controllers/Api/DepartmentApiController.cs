using LMS.Models.Feature.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers.Api
{
   
    public class DepartmentApiController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public object GetListData()
        {
            var temp = new GetListingRequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpGet]
        public object GetDepartmentsDropdown()
        {
            var temp = new GetDepartmentsDropdownRequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpGet]
        public object GetDepartmentsDropdownForPO()
        {
            var temp = new GetDepartmentsDropdownForPORequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpPost]
        public object AddDepartment( AddDepartmentRequest req) //If not working remove frombody
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetDepartment([FromUri] GetDepartmentRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditDepartment(EditDepartmentRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
    }
}