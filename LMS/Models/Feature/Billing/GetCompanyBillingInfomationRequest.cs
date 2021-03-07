using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Billing
{
    
	public class GetCompanyBillingInfomationRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; } //CompanyId
		public object RunRequest(GetCompanyBillingInfomationRequest req)
		{
			var response = new GetCompanyBillingInfomationResponse();
            response.BillingDetails = new List<BillingDetails>();
            response.BillingInformation = new BillingInformation();

            var BI = _dbContext.Company.Where(x=>x.Id== req.Id).FirstOrDefault();
            if (BI.BillingInformation != null)
            {
                response.BillingInformation.Id = BI.BillingInformation.Id;
                response.BillingInformation.Address1 = BI.BillingInformation.Address1;
                response.BillingInformation.Address2 = BI.BillingInformation.Address2;
                response.BillingInformation.BillingFormatType = BI.BillingInformation.BillingFormatType;
                response.BillingInformation.City = BI.BillingInformation.City;
                response.BillingInformation.CompanyContact = BI.BillingInformation.CompanyContact;
                response.BillingInformation.CompanyEmail = BI.BillingInformation.CompanyEmail;
                response.BillingInformation.Country = BI.BillingInformation.Country;
                response.BillingInformation.DeliveryMode = BI.BillingInformation.DeliveryMode;
                response.BillingInformation.POCContact = BI.BillingInformation.POCNumber;
                response.BillingInformation.POCName = BI.BillingInformation.POCName;
                response.BillingInformation.State = BI.BillingInformation.State;
                response.BillingInformation.Zip = BI.BillingInformation.Zip;
                response.BillingInformation.NTN = BI.BillingInformation.NTN;

                if (BI.BillingInformation.BillingDetails != null)
                {
                    foreach (var BDetails in BI.BillingInformation.BillingDetails)
                    {
                        var BillingDetails = new BillingDetails();
                        BillingDetails.BillingName = BDetails.BillingName;
                        BillingDetails.BillingInformationId = BDetails.BillingInformationId;
                        BillingDetails.OTC = BDetails.OTC;
                        BillingDetails.MRC = BDetails.MRC;
                        BillingDetails.Medium = BDetails.Medium;
                        BillingDetails.BillingStatus = BDetails.BillingStatus; //enum conversion also needed
                        BillingDetails.BirthDate = BDetails.BirthDate;
                        BillingDetails.CreatedAt = BDetails.CreatedAt;
                        BillingDetails.CreatedBy = BDetails.CreatedBy;
                        BillingDetails.Date = BDetails.Date;
                        BillingDetails.Id = BDetails.Id;
                        BillingDetails.InstallationDate = BDetails.InstallationDate;
                        BillingDetails.IsDirectBilling = BDetails.IsDirectBilling;
                        BillingDetails.IsPaperBillRequired = BDetails.IsPaperBillRequired;
                        BillingDetails.Package = BDetails.Package;
                        BillingDetails.SalesAgentId = BDetails.SalesAgentId;
                        BillingDetails.UpdatedAt = BDetails.UpdatedAt;
                        BillingDetails.UpdatedBy = BDetails.UpdatedBy;
                        response.BillingDetails.Add(BillingDetails);

                    }
                }
            }
            return response;
		}
	}
	public class BillingInformation
    {
        public int Id { get; set; }
        public string NTN { get; set; }
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

	}
	public class BillingDetails
    {
        public int Id { get; set; }
        public string BillingName { get; set; }
        public int BillingInformationId { get; set; }
  
        public int? SalesAgentId { get; set; }
        public bool? IsPaperBillRequired { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public decimal OTC { get; set; }
        public decimal MRC { get; set; }
        public string Package { get; set; }
        public string Medium { get; set; }
        public string Description { get; set; }
        public int? BillingStatus { get; set; }
        public bool IsDirectBilling { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? Date { get; set; }

    }
	public class GetCompanyBillingInfomationResponse
	{
		public BillingInformation BillingInformation { get; set; }
        public List<BillingDetails> BillingDetails { get; set; }

    }
}