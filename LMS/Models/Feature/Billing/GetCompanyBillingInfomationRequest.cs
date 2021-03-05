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

                if (BI.BillingInformation.BillingDetails != null)
                {
                    foreach (var BDetails in BI.BillingInformation.BillingDetails)
                    {
                        var BillingDetails = new BillingDetails();
                        BillingDetails.BillingName = BDetails.BillingName;
                        BillingDetails.AgeRange = BDetails.AgeRange;
                        BillingDetails.BillDeliveryCrStatusDate = BDetails.BillDeliveryCrStatusDate;
                        BillingDetails.BillingInformationId = BDetails.BillingInformationId;
                        BillingDetails.BillingStatus = BDetails.BillingStatus; //enum conversion also needed
                        BillingDetails.BillReceivedByCrStatus = BDetails.BillReceivedByCrStatus;
                        BillingDetails.BirthDate = BDetails.BirthDate;
                        BillingDetails.CollectionAgent = BDetails.CollectionAgent;
                        BillingDetails.CPEType = BDetails.CPEType;
                        BillingDetails.CreatedAt = BDetails.CreatedAt;
                        BillingDetails.CreatedBy = BDetails.CreatedBy;
                        BillingDetails.CSAFNumber = BDetails.CSAFNumber;
                        BillingDetails.CustomerNTN = BDetails.CustomerNTN;
                        BillingDetails.CustomerStr = BDetails.CustomerStr;
                        BillingDetails.Date = BDetails.Date;
                        BillingDetails.Id = BDetails.Id;
                        BillingDetails.InfinityMappedQubeePackageId = BDetails.InfinityMappedQubeePackageId; //enum conversion needed
                        BillingDetails.InfinityMigrationCategory = BDetails.InfinityMigrationCategory;
                        BillingDetails.InfinityRefAccNo = BDetails.InfinityRefAccNo;
                        BillingDetails.InfinityRefCPEType = BDetails.InfinityRefCPEType;
                        BillingDetails.InfinityRefMLR = BDetails.InfinityRefMLR;
                        BillingDetails.InfinityUnsuccessfulMigrationReason = BDetails.InfinityUnsuccessfulMigrationReason;
                        BillingDetails.InstallationDate = BDetails.InstallationDate;
                        BillingDetails.InstalledBy = BDetails.InstalledBy;
                        BillingDetails.IsDirectBilling = BDetails.IsDirectBilling;
                        BillingDetails.IsInitialPaymentReceived = BDetails.IsInitialPaymentReceived;
                        BillingDetails.IsPaperBillRequired = BDetails.IsPaperBillRequired;
                        BillingDetails.MGM_ExistingCustomerSng = BDetails.MGM_ExistingCustomerSng;
                        BillingDetails.NationalIdNo = BDetails.NationalIdNo;
                        BillingDetails.Package = BDetails.Package;
                        BillingDetails.PassportNo = BDetails.PassportNo;
                        BillingDetails.Reason = BDetails.Reason;

                        BillingDetails.RefAddress1 = BDetails.RefAddress1;
                        BillingDetails.RefAddress2 = BDetails.RefAddress2;

                        BillingDetails.RefCNIC1 = BDetails.RefCNIC1;
                        BillingDetails.RefCNIC2 = BDetails.RefCNIC2;
                        BillingDetails.RefContact1 = BDetails.RefContact1;
                        BillingDetails.RefContact2 = BDetails.RefContact2;
                        BillingDetails.RefEmail1 = BDetails.RefEmail1;
                        BillingDetails.RefEmail2 = BDetails.RefEmail2;
                        BillingDetails.RefName1 = BDetails.RefName1;
                        BillingDetails.RefName2 = BDetails.RefName2;
                        BillingDetails.RetailOutletID = BDetails.RetailOutletID;
                        BillingDetails.SalesAgentId = BDetails.SalesAgentId;
                        BillingDetails.SalesRepId = BDetails.SalesRepId;
                        BillingDetails.UpdatedAt = BDetails.UpdatedAt;
                        BillingDetails.UpdatedBy = BDetails.UpdatedBy;
                        BillingDetails.VerificationRemarks = BDetails.VerificationRemarks;
                        BillingDetails.VerificationStatus = BDetails.VerificationStatus;
                        BillingDetails.WifiRouter = BDetails.WifiRouter;
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
        public string CustomerStr { get; set; }
        public int? SalesAgentId { get; set; }
        public bool? IsPaperBillRequired { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string CPEType { get; set; }
        public string VerificationStatus { get; set; }
        public bool? IsInitialPaymentReceived { get; set; }
        public string BillReceivedByCrStatus { get; set; }
        public string VerificationRemarks { get; set; }
        public string InstalledBy { get; set; }
        public string MGM_ExistingCustomerSng { get; set; }
        public string InfinityMigrationCategory { get; set; }
        public string InfinityRefMLR { get; set; }
        public string CollectionAgent { get; set; }
        public string NationalIdNo { get; set; }
        public string PassportNo { get; set; }
        public int? RetailOutletID { get; set; }
        public string AgeRange { get; set; }
        public string InfinityRefAccNo { get; set; }
        public int? InfinityMappedQubeePackageId { get; set; }
        public DateTime? BillDeliveryCrStatusDate { get; set; }
        public string RefName1 { get; set; }
        public string RefContact1 { get; set; }
        public string RefEmail1 { get; set; }
        public string RefCNIC1 { get; set; }
        public string RefAddress1 { get; set; }
        public string RefName2 { get; set; }
        public string RefContact2 { get; set; }
        public string RefEmail2 { get; set; }
        public string RefCNIC2 { get; set; }
        public string RefAddress2 { get; set; }
        public string CSAFNumber { get; set; }
        public string Reason { get; set; }
        public string InfinityRefCPEType { get; set; }
        public string CustomerNTN { get; set; }
        public string WifiRouter { get; set; }
        public string Package { get; set; }
        public string InfinityUnsuccessfulMigrationReason { get; set; }
        public int? SalesRepId { get; set; }
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