using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Charge
{
   
    public class AddChargeResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddChargeRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int Type { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


        public object RunRequest(AddChargeRequest request)
        {
            var response = new AddChargeResponse();
            var Charge = new LMS.Models.EntityModel.Charge();
            Charge.Type = request.Type;
            Charge.Name = request.Name;
            Charge.Value = request.Value;
            Charge.CreatedAt = DateTime.Today;
            Charge.CreatedBy = request.CreatedBy;
            var result = _dbContext.Charge.Add(Charge);
            _dbContext.SaveChanges();
            return response;
        }
    }
}