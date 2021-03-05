using LMS.Models.Feature.Ticket;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LMS.Controllers.Api
{
    public class TicketApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }


        [HttpPost]
        public object AddTicket([FromBody] AddTicketRequest req) 
        {
            req.CreatedBy = User.Identity.Name;
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }


        [HttpPost]
        public object EditTicket([FromBody] EditTicketRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetTicket([FromUri] GetTicketRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetTicketComments([FromUri] GetTicketCommentsRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddTicketComments([FromBody] AddTicketCommentsRequest req)
        {
            req.CreatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EditTicketComments([FromBody] EditTicketCommentsRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ChangeTicketStatus([FromBody] ChangeTicketStatusRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }


    }

}