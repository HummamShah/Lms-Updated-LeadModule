using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanySubcription
{
    public class GetListingResponse
    {
        public List<CompanySubcriptionData> Data { get; set; }
    }

    public class CompanySubcriptionData
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public decimal? OTC { get; set; }
        public decimal? MRC { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int? Id { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<CompanySubcriptionData>();
            var Data = _dbContext.CompanySubcription.ToList();
            if (req.Id.HasValue)
            {
                Data = Data.Where(x => x.CompanyId == req.Id).OrderBy(x => x.CompanyId).ToList();
            }
            foreach (var d in Data)
            {
                var CompanySubcription = new CompanySubcriptionData();
                CompanySubcription.Id = d.Id;
                CompanySubcription.Name = d.Name;
                CompanySubcription.CompanyId = d.CompanyId;
                CompanySubcription.CompanyName = d.Company.Name;
                CompanySubcription.OTC = d.OTC;
                CompanySubcription.MRC = d.MRC;
                CompanySubcription.DateFrom = d.DateFrom;
                CompanySubcription.DateTo = d.DateTo;
                CompanySubcription.Description = d.Description;
                CompanySubcription.CreatedAt = d.CreatedAt;
                CompanySubcription.CreatedBy = d.CreatedBy;
                response.Data.Add(CompanySubcription);
            }
            return response;

        }
    }
}