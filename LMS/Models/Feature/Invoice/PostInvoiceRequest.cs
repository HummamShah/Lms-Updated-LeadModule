using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Invoice
{
  
	public class PostInvoiceResponse
	{
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class PostInvoiceRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public string CreatedBy { get; set; }
		public object RunRequest(PostInvoiceRequest request)
		{
			var response = new PostInvoiceResponse();
			var IsAccountEntriesPosted = true;

			var Invoice = _dbContext.Invoice.Where(x => x.Id == request.Id).FirstOrDefault();
            if (Invoice.Account != null)
            {
                try
                {
					foreach (var InvoiceDetail in Invoice.InvoiceDetails)
					{
						var AccountDetail = new AccountEntries(); // need to add field amount in this table
						AccountDetail.AccountId = Invoice.AccountId.Value;
						AccountDetail.CreatedAt = DateTime.Now;
						AccountDetail.CreatedBy = request.CreatedBy;
						AccountDetail.Date = DateTime.Now.Date;
						AccountDetail.Debit = InvoiceDetail.Amount??0;
						AccountDetail.Description = InvoiceDetail.Description;

						var Result = _dbContext.AccountEntries.Add(AccountDetail);
						var DbResult = _dbContext.SaveChanges();
					}
					foreach (var InvoiceDetail in Invoice.ChargeDetails)
					{
						var AccountDetail = new AccountEntries(); // need to add field amount in this table
						AccountDetail.AccountId = Invoice.AccountId.Value;
						AccountDetail.CreatedAt = DateTime.Now;
						AccountDetail.CreatedBy = request.CreatedBy;
						AccountDetail.Date = DateTime.Now.Date;
						AccountDetail.Debit = InvoiceDetail.Amount??0;
						AccountDetail.Description = InvoiceDetail.Name;
						var Result = _dbContext.AccountEntries.Add(AccountDetail);
						var DbResult = _dbContext.SaveChanges();
					}
				}
				catch(Exception ex)
                {
					IsAccountEntriesPosted = false;

				}
            
		}
            if (IsAccountEntriesPosted)
            {
				Invoice.IsPosted = true;
				Invoice.Status = (int)InvoiceStatus.Completed;
				decimal Balance = 0;
                if (Invoice.Account.Balance.HasValue)
                {
					Balance = Invoice.Account.Balance.Value;

				}
				Invoice.Account.Balance = Balance + Invoice.TotalAmount;
				_dbContext.SaveChanges();
			}
			return response;
		}
	}
}