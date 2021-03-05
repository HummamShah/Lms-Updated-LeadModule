using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Payment
{
    public class GetListingResponse
    {
        public List<PaymentData> Data { get; set; }
    }

    public class PaymentData
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int PaymentType { get; set; }
        public string CheckNumber { get; set; }
        public int Status { get; set; }
        public DateTime? CheckDate { get; set; }
        public string DepositedBank { get; set; }
        public string BankName { get; set; }
        public string StatusEnum { get; set; }
        public string PaymentTypeEnum { get; set; }
        public bool? IsPosted { get; set; }
        public decimal? Discount { get; set; }
        

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }


    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
    
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<PaymentData>();
            var Data = _dbContext.Payment;

            foreach (var d in Data)
            {
                var Payment = new PaymentData();
                Payment.Id = d.Id;
                Payment.AccountId = d.AccountId??0;
                Payment.AccountName = d.Account.Name;
                Payment.Amount = d.Amount;
                Payment.CheckNumber = d.CheckNumber;
                Payment.CompanyId = d.CompanyId;
                Payment.CompanyName = d.Company.Name;
                Payment.Description = d.Description;
                Payment.IsPosted = d.IsPosted;
                Payment.PaymentType = d.PaymentType;
                Payment.PaymentTypeEnum = ((PaymentType)d.PaymentType).ToString();
                Payment.Status = d.Status;
                Payment.StatusEnum = ((PaymentStatus)d.Status).ToString();
                Payment.CreatedAt = d.CreatedAt;
                Payment.CreatedBy = d.CreatedBy;
                response.Data.Add(Payment);
            }
            return response;

        }
    }
}