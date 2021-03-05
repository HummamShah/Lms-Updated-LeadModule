using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Payment
{
	public class PostPaymentResponse
	{
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class PostPaymentRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public string CreatedBy { get; set; }
		public object RunRequest(PostPaymentRequest request)
		{
			var response = new PostPaymentResponse();
			var IsAccountEntriesPosted = true;

			var Payment = _dbContext.Payment.Where(x => x.Id == request.Id).FirstOrDefault();
			if (Payment.Account != null)
			{
				try
				{
					var AccountDetail = new AccountEntries(); // need to add field amount in this table
					AccountDetail.AccountId = Payment.AccountId.Value;
					AccountDetail.CreatedAt = DateTime.Now;
					AccountDetail.CreatedBy = request.CreatedBy;
					AccountDetail.Date = DateTime.Now.Date;
					AccountDetail.Credit = Payment.Amount;
					AccountDetail.Debit = 0;
					AccountDetail.Description = Payment.Description;
					var Result = _dbContext.AccountEntries.Add(AccountDetail);
					var DbResult = _dbContext.SaveChanges();
					
				}
				catch (Exception ex)
				{
					IsAccountEntriesPosted = false;

				}

			}
			if (IsAccountEntriesPosted)
			{
				Payment.IsPosted = true;
				Payment.Status = (int)PaymentStatus.Completed;
				decimal Balance = 0;
				if (Payment.Account.Balance.HasValue)
				{
					Balance = Payment.Account.Balance.Value;

				}
				Payment.Account.Balance = Balance - Payment.Amount;
				_dbContext.SaveChanges();
			}
			return response;
		}
	}
}