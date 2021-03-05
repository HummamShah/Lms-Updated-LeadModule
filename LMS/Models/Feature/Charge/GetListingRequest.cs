using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Charge
{
    public class GetListingResponse
    {
        public List<ChargeData> Data { get; set; }
    }

    public class ChargeData
    {
        public int Id { get; set; }
        public int ChargeId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int Type { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<ChargeData>();
            var Data = _dbContext.Charge.Where(x=>x.Type == req.Type);

            foreach (var d in Data)
            {
                var Charge = new ChargeData();
                Charge.Id = d.Id;
                Charge.Name = d.Name;
                Charge.Value = d.Value;
                Charge.CreatedAt = d.CreatedAt;
                Charge.CreatedBy = d.CreatedBy;
                response.Data.Add(Charge);
            }
            return response;

        }
    }
}