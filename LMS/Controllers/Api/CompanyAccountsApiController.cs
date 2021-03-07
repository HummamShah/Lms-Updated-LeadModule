using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LMS.Models.Feature.CompanyAccounts;

namespace LMS.Controllers.Api
{
    public class CompanyAccountsApiController : ApiController
    {

        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

       
        [HttpGet]
        public object GetParentListData([FromUri] GetParentListDataRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetChildListData([FromUri] GetChildListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCompanyAccountsDetail([FromUri] GetCompanyAccountsRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetCompanyAccountsDropdown([FromUri] GetCompanyAccountsDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}