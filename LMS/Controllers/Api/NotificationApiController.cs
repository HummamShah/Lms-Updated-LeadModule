using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LMS.Models.Feature.Notification;
using Microsoft.AspNet.Identity;

namespace LMS.Controllers.Api
{
    public class NotificationApiController : ApiController
    {
        [HttpGet]
        public object GetListData([FromUri] GetListingRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetTopFiveNotifications([FromUri] GetTopFiveNotificationsRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

    }
}