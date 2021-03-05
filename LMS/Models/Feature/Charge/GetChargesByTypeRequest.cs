using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Charge
{
    public class GetChargesByTypeResponse
    {
        public List<ChargeData> Charges { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class GetChargesByTypeRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string Type { get; set; }
        public object RunRequest(GetChargesByTypeRequest request)
        {
            var response = new GetChargesByTypeResponse();
            response.Charges = new List<ChargeData>();
            var type =(int) (ChargeType)Enum.Parse(typeof(ChargeType), request.Type);
            var Charges = _dbContext.Charge.Where(x => x.Type == type);
            foreach (var Charge in Charges)
            {
                var ChargesData = new ChargeData();
                ChargesData.ChargeId = Charge.Id;
                ChargesData.Type = Charge.Type;
                ChargesData.Name = Charge.Name;
                ChargesData.Value = Charge.Value;
                response.Charges.Add(ChargesData);
            }
         
            return response;
        }
    }
}