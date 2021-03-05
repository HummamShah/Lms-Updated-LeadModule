using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Charge
{
    
    public class EditChargeResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class EditChargeRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string UpdatedBy { get; set; }


        public object RunRequest(EditChargeRequest request)
        {
            var response = new AddChargeResponse();
            var Charge =_dbContext.Charge.Where(x=>x.Id == request.Id).FirstOrDefault();
            Charge.Type = request.Type;
            Charge.Name = request.Name;
            Charge.Value = request.Value;
            Charge.UpdatedAt = DateTime.Today;
            Charge.UpdatedBy = request.UpdatedBy;
            _dbContext.SaveChanges();
            return response;
        }
    }
}