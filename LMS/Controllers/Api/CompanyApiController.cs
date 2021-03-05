using LMS.Models.Feature.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using LMS.Models.Enums;

namespace LMS.Controllers.Api
{
    public class CompanyApiController : ApiController
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
        public object GetAllCompaniesDropDown()
        {
            var temp = new GetAllCompaniesDropDownRequest();
            var result = temp.RunRequest();
            return result;
        }
        [HttpPost]
        public object AddCompany([FromBody] AddCompanyRequest req) //If not working remove frombody
        {
            req.CreatedBy = User.Identity.Name;
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCompany([FromUri] GetCompanyRequest req)
        {
            
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditCompany(EditCompanyRequest req) 
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetParentCompaniesDropdown()
        {
            var req = new GetParentCompaniesDropdownRequest();
            var result = req.RunRequest();
            return result;
        }

        [HttpGet]
        public object GetHeadParentCompaniesDropdown()
        {
            var req = new GetHeadParentCompaniesDropdownRequest();
            var result = req.RunRequest();
            return result;
        }
        [HttpGet]
        public object GetCompaniesDropdown(int LeadId)
        {
        
            var req = new GetCompaniesDropdownRequest();
            req.LeadId = LeadId;
            if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.SuperAdmin))
            {
                req.UserId = User.Identity.GetUserId();
            }
            var result = req.RunRequest(req);
            return result;
        }


        //[HttpGet]
        //public object GetCompaniesAccountDropdown(GetCompaniesAccountDropdownRequest req)
        //{
        //   // var req = new GetCompaniesAccountDropdownRequest();
        //    var result = req.RunRequest(req);
        //    return result;
        //}
        [HttpGet]
        public object GetAccountByCompanyId([FromUri] GetAccountByCompanyIdRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object TagAgent(TagAgentRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}