using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Payment
{
    
    public class EditPaymentResponse
    {
       public List<string> ValidationError { get; set; }
    }
    public class EditPaymentRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task

        public int Id { get; set; }
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
        public string UpdatedBy { get; set; }

        public object RunRequest(EditPaymentRequest req)
        {
            var response = new EditPaymentResponse();
            
            var Payment = _dbContext.Payment.Where(x => x.Id == req.Id).FirstOrDefault();
            Payment.Id = req.Id;
            Payment.AccountId = req.AccountId;
           
            Payment.Amount = req.Amount;
            Payment.CheckNumber = req.CheckNumber;
            Payment.CompanyId = req.CompanyId;
            Payment.CheckDate = req.CheckDate;
            Payment.BankName = req.BankName;
            Payment.DepositedBank = req.DepositedBank;
            Payment.Description = req.Description;
            Payment.IsPosted = false;
            Payment.PaymentType = req.PaymentType;

            Payment.Status = (int)PaymentStatus.Pending;
            Payment.UpdatedAt = DateTime.Now;
            Payment.UpdatedBy =req.UpdatedBy;
            _dbContext.SaveChanges();
            return response;

        }
    }
}