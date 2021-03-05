using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Invoice
{
    public class GetListingResponse
    {
        public List<InvoiceListData> Data { get; set; }
    }

    public class InvoiceListData
    {
        public int Id { get; set; }
        public bool IsPosted { get; set; }
        public int Status { get; set; }
        public string StatusEnum { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? AccountId { get; set; }
        public string AttendantName { get; set; }
        public string AccountName { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }



    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<InvoiceListData>();
            var Data = _dbContext.Invoice;

            foreach (var d in Data)
            {
                var Invoice = new InvoiceListData();
                Invoice.Id = d.Id;
                Invoice.AccountId = d.AccountId;
                if (d.AccountId.HasValue)
                {
                    Invoice.AccountName = d.Account.Name;
                }
                Invoice.AttendantName = d.AttendantName;
                Invoice.CompanyId = d.CompanyId;
                Invoice.CompanyName = d.Company.Name;
                Invoice.IsPosted = d.IsPosted;
                Invoice.Status = d.Status;
                Invoice.StatusEnum = ((InvoiceStatus)d.Status).ToString();
                Invoice.TotalAmount = d.TotalAmount;
                Invoice.StartDate = d.StartDate;
                Invoice.EndDate = d.EndDate;
                Invoice.CreatedAt = d.CreatedAt;
                Invoice.CreatedBy = d.CreatedBy;
                response.Data.Add(Invoice);
            }
            return response;

        }
    }
}