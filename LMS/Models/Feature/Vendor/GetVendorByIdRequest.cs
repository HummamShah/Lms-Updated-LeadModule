using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Vendor
{
    public class GetVendorByIdRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }

        public object RunRequest(GetVendorByIdRequest req)
        {
            var response = new GetVendorResponse();
            var Vendor = _dbContext.Vendor.Where(x => x.Id == req.Id).FirstOrDefault();
            response.Id = Vendor.Id;
            response.Name = Vendor.Name;
            response.Description = Vendor.Description;
            response.CreatedBy = Vendor.CreatedBy;
            response.CreatedAt = Vendor.CreatedAt;
            response.UpdatedBy = Vendor.UpdatedBy;
            response.UpdatedAt = Vendor.UpdatedAt;
            return response;
        }
    }

    public class GetVendorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}