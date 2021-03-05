using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Payment
{
 
    public class AddPaymentResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddPaymentRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int PaymentType { get; set; }
        public string CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public string DepositedBank { get; set; }
        public string BankName { get; set; }
        public decimal? Discount { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }


        public object RunRequest(AddPaymentRequest request)
        {
            var response = new AddPaymentResponse();
            var Payment = new LMS.Models.EntityModel.Payment();
            Payment.IsPosted = false;
            Payment.Status = (int)PaymentStatus.Pending;
            Payment.PaymentType = request.PaymentType;
            Payment.AccountId = request.AccountId;
            Payment.Amount = request.Amount;
            Payment.CheckNumber = request.CheckNumber;
            Payment.CheckDate = request.CheckDate;
            Payment.BankName = request.BankName;
            Payment.DepositedBank = request.DepositedBank;
            Payment.CompanyId = request.CompanyId;
            Payment.Description = request.Description;
            Payment.Discount = request.Discount;
            Payment.CreatedAt = DateTime.Today;
            Payment.CreatedBy = request.CreatedBy;
            Payment.Date = DateTime.Now.Date;
            var result = _dbContext.Payment.Add(Payment);
            _dbContext.SaveChanges();
            return response;
        }
    }
}