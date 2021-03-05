using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Billing
{
	public class GetListingResponse
	{
		public List<CompanyBillingData> Data { get; set; }
	}
	public class CompanyBillingData
	{
		public int Id { get; set; }
		public string CompanyName { get; set; }
		public int CompanyId { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string BillingFormatType { get; set; }
		public string POCName { get; set; }
		public string POCContact { get; set; }
		public string CompanyContact { get; set; }
		public string CompanyEmail { get; set; }
		public string DeliveryMode { get; set; }
		public int? BillingStatus { get; set; }
		public string BillingStatusEnum { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string CreatedBy { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		//Pagination Fields
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public object RunRequest(GetListingRequest request)
		{
			var response = new GetListingResponse();
			response.Data = new List<CompanyBillingData>();
			//var Companies = _dbContext.Company.Where(x=>x.BillingInformationId != null);
			var Companies = _dbContext.Company;
			foreach (var Company in Companies)
			{
				var CompanyBilling = new CompanyBillingData();
				CompanyBilling.Id = Company.Id;
				CompanyBilling.CompanyId = Company.Id;
				CompanyBilling.CompanyName = Company.Name;
                //Billing Information
                if (Company.BillingInformationId != null)
                {
					CompanyBilling.Address1 = Company.BillingInformation.Address1;
					CompanyBilling.Address2 = Company.BillingInformation.Address2;
					CompanyBilling.BillingFormatType = Company.BillingInformation.BillingFormatType;
					CompanyBilling.BillingStatus = Company.BillingInformation.BillingStatus;
					//CompanyBilling.BillingStatusEnum = ((BillingStatus)Company.BillingInformation.BillingStatus).ToString();
					CompanyBilling.City = Company.BillingInformation.City;
					CompanyBilling.CompanyContact = Company.BillingInformation.CompanyContact;
					CompanyBilling.CompanyEmail = Company.BillingInformation.CompanyEmail;
					CompanyBilling.Country = Company.BillingInformation.Country;
					CompanyBilling.DeliveryMode = Company.BillingInformation.DeliveryMode;
					CompanyBilling.POCName = Company.BillingInformation.POCName;
					CompanyBilling.CreatedAt = Company.BillingInformation.CreatedAt;
					CompanyBilling.CreatedBy = Company.BillingInformation.CreatedBy;
				}
				
				response.Data.Add(CompanyBilling);
			}
			return response;
		}
	}
}