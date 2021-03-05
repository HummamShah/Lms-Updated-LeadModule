using LMS.Models.Feature.Lead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using LMS.Models.Enums;

namespace LMS.Controllers.Api
{
    public class LeadApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest temp)
        {
            //var temp = new GetListingRequest();
            if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.SuperAdmin))
            {
                temp.UserId = User.Identity.GetUserId();
            }
            
            var result = temp.RunRequest(temp);
            return result;
        }


        [HttpGet]
        public object GetListForPmd()
        {
            var temp = new GetListingForPmdRequest();
            if (!User.IsInRole(Roles.Admin) && !User.IsInRole(Roles.SuperAdmin))
            {
                temp.UserId = User.Identity.GetUserId();
            }

            var result = temp.RunRequest(temp);
            return result;
        }
        [HttpPost]
        public object AddLead([FromBody] AddLeadRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddLeadNew([FromBody] AddLeadNewRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddFeasibility([FromBody] AddFeasibilityRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditFeasibility([FromBody] EditFeasibilityRequest req) //If not working remove frombody
        {
            req.UserId = User.Identity.GetUserId();
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AssignLead([FromBody] AssignLeadRequest req) //If not working remove frombody
        {

            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EditLead(EditLeadRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditNewLead(EditNewLeadRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]

        public object GetLead([FromUri] GetLeadRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]

        public object GetNewLead([FromUri] GetLeadNewRequest req)
        {

            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetQuestionnareData([FromUri] GetQuestionnareDataRequest req)
        {
            var result = req.RunRequest(req);
            return result;

        }
        [HttpPost]
        public object SaveQuestionnareData([FromBody] SaveQuestionnareDataRequest req)
        {
            var result = req.RunRequest(req);
            return result;

        }
        [HttpPost]
        public object AddQuotation([FromBody] AddQuotationRequest req)
        {
            var result = req.RunRequest(req);
            return result;

        }
        [HttpPost]
        public object AddApproval([FromBody] AddApprovalRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EditLeadStatusRequest([FromBody] EditLeadStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ChangeLeadStatus([FromBody] ChangeLeadStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetLeadById([FromUri] GetLeadByIdRequest req)
        {
            var result = req.RunRequest(req);
            return result;

        }
        [HttpPost]
        public object ChangeFeasibilityStatus([FromBody] ChangeFeasibilityStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }


    }
}