using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanySubcription
{
  
    public class AddCompanySubcriptionResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddCompanySubcriptionRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

      
        public int CompanyId { get; set; }
        public decimal? OTC { get; set; }
        public decimal? MRC { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }


        public object RunRequest(AddCompanySubcriptionRequest request)
        {
            var response = new AddCompanySubcriptionResponse();
            var Subcription = new LMS.Models.EntityModel.CompanySubcription();
            Subcription.CompanyId = request.CompanyId;
            Subcription.CreatedBy = request.CreatedBy;
            Subcription.Date = DateTime.Now.Date;
            Subcription.DateFrom = request.DateFrom;
            Subcription.DateTo = request.DateTo;
            Subcription.Description = request.Description;
            Subcription.MRC = request.MRC;
            Subcription.Name = request.Name;
            Subcription.OTC = request.OTC;
            Subcription.CreatedAt = DateTime.UtcNow;
             var result = _dbContext.CompanySubcription.Add(Subcription);
            _dbContext.SaveChanges();
            return response;
        }
    }
}