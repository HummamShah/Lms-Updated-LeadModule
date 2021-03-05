using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Payment
{
   
    public class GetPaymentResponse
    {
        public PaymentData Payment { get; set; }
    }

  

    public class GetPaymentRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task

        public int Id { get; set; }
       
        public object RunRequest(GetPaymentRequest req)
        {
            var response = new GetPaymentResponse();
            response.Payment = new PaymentData();
            var PaymentRow = _dbContext.Payment.Where(x=>x.Id == req.Id).FirstOrDefault();
            var Payment = new PaymentData();
            Payment.Id = PaymentRow.Id;
            Payment.AccountId = PaymentRow.AccountId ?? 0;
            Payment.AccountName = PaymentRow.Account.Name;
            Payment.Amount = PaymentRow.Amount;
            Payment.CheckNumber = PaymentRow.CheckNumber;
            Payment.CheckDate = PaymentRow.CheckDate;
            Payment.BankName = PaymentRow.BankName;
            Payment.DepositedBank = PaymentRow.DepositedBank;
            Payment.CompanyId = PaymentRow.CompanyId;
            Payment.CompanyName = PaymentRow.Company.Name;
            Payment.Description = PaymentRow.Description;
            Payment.IsPosted = PaymentRow.IsPosted;
            Payment.PaymentType = PaymentRow.PaymentType;
            Payment.PaymentTypeEnum = ((PaymentType)PaymentRow.PaymentType).ToString();
            Payment.Status = PaymentRow.Status;
            Payment.StatusEnum = ((PaymentStatus)PaymentRow.Status).ToString();
            Payment.CreatedAt = PaymentRow.CreatedAt;
            Payment.CreatedBy = PaymentRow.CreatedBy;
            response.Payment = Payment;
            return response;

        }
    }
}