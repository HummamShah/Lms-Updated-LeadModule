using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Charge
{
 
    public class GetChargeResponse
    {
        public ChargeData Charge { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class GetChargeRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public object RunRequest(GetChargeRequest request)
        {
            var response = new GetChargeResponse();
            var Charge = _dbContext.Charge.Where(x => x.Id == request.Id).FirstOrDefault();
            var ChargesData = new ChargeData();
            ChargesData.Id = Charge.Id;
            ChargesData.Type = Charge.Type;
            ChargesData.TypeName = ((ChargeType)Charge.Type).ToString();
            ChargesData.Name = Charge.Name;
            ChargesData.Value = Charge.Value;
            ChargesData.CreatedAt = Charge.CreatedAt;
            ChargesData.CreatedBy = Charge.CreatedBy;
            response.Charge = new ChargeData();
            response.Charge = ChargesData;
            return response;
        }
    }
}