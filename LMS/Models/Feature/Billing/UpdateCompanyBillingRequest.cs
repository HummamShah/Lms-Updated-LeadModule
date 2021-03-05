using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Billing
{

    public class UpdateCompanyBillingResponse
    {

        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class BillingData
    {
        public int CompanyId { get; set; }
        public BillingInformation BillingInformation { get; set; }
        public List<BillingDetails> BillingDetails { get; set; }
    }
    public class UpdateCompanyBillingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public string UpdatedBy { get; set; }
        public BillingData BillingData { get; set; }
        public object RunRequest(UpdateCompanyBillingRequest request)
        {
            var response = new UpdateCompanyBillingResponse();

            var BillingInformation = new LMS.Models.EntityModel.BillingInformation();
            var Billinginfoid = request.BillingData.BillingInformation.Id;
            if (request.BillingData.BillingInformation.Id != 0)
            {
                BillingInformation = _dbContext.BillingInformation.Where(x => x.Id == request.BillingData.BillingInformation.Id).FirstOrDefault();
                //update here 
                BillingInformation.Address1 = request.BillingData.BillingInformation.Address1;
                BillingInformation.Address2 = request.BillingData.BillingInformation.Address2;
                BillingInformation.BillingFormatType = request.BillingData.BillingInformation.BillingFormatType;
                //BillingInformation.BillingStatus = request.BillingData.BillingInformation.BillingStatus;
                BillingInformation.City = request.BillingData.BillingInformation.City;
                BillingInformation.CompanyContact = request.BillingData.BillingInformation.CompanyContact;
                BillingInformation.CompanyEmail = request.BillingData.BillingInformation.CompanyEmail;
                BillingInformation.Country = request.BillingData.BillingInformation.Country;
                BillingInformation.Zip = request.BillingData.BillingInformation.Zip;
                BillingInformation.POCName = request.BillingData.BillingInformation.POCName;
                BillingInformation.POCNumber = request.BillingData.BillingInformation.POCContact;
                BillingInformation.DeliveryMode = request.BillingData.BillingInformation.DeliveryMode;
                BillingInformation.UpdatedBy = request.UpdatedBy;
                BillingInformation.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
            }
            else
            {
                //Add new here and then create company account aswell
               
                BillingInformation.Address1 = request.BillingData.BillingInformation.Address1;
                BillingInformation.Address2 = request.BillingData.BillingInformation.Address2;
                BillingInformation.BillingFormatType = request.BillingData.BillingInformation.BillingFormatType;
                //BillingInformation.BillingStatus = request.BillingData.BillingInformation.BillingStatus;
                BillingInformation.City = request.BillingData.BillingInformation.City;
                BillingInformation.CompanyContact = request.BillingData.BillingInformation.CompanyContact;
                BillingInformation.CompanyEmail = request.BillingData.BillingInformation.CompanyEmail;
                BillingInformation.Country = request.BillingData.BillingInformation.Country;
                BillingInformation.Zip = request.BillingData.BillingInformation.Zip;
                BillingInformation.POCName = request.BillingData.BillingInformation.POCName;
                BillingInformation.POCNumber = request.BillingData.BillingInformation.POCContact;
                BillingInformation.DeliveryMode = request.BillingData.BillingInformation.DeliveryMode;
                BillingInformation.CreatedBy = request.UpdatedBy;
                BillingInformation.CreatedAt = DateTime.Now;
                BillingInformation.Date = DateTime.Now.Date;
                var result = _dbContext.BillingInformation.Add(BillingInformation);
                var company = _dbContext.Company.Where(x => x.Id == request.BillingData.CompanyId).FirstOrDefault();
                company.BillingInformationId = Billinginfoid;
                //Account Creation
                var Account = new Account();
                Account.CreatedAt = DateTime.Now;
                Account.Date = DateTime.Now.Date;
                Account.CreatedBy = request.UpdatedBy;
                Account.CompanyId = request.BillingData.CompanyId;
                Account.IsActive = true;
                Account.Name = "ACC_" + request.BillingData.CompanyId + "_"+ company.Name;
                var AccountResult = _dbContext.Account.Add(Account);
                _dbContext.SaveChanges();
                Billinginfoid = result.Id;
            }
            //companyId assignation


            foreach (var BillingDetailRow in request.BillingData.BillingDetails)
            {
                var BillingDetail = new LMS.Models.EntityModel.BillingDetails();
                if (BillingDetailRow.Id != 0)
                {
                    //Update here
                    BillingDetail = _dbContext.BillingDetails.Where(x => x.Id == BillingDetailRow.Id).FirstOrDefault();
                    BillingDetail.BillingName = BillingDetailRow.BillingName;
                    BillingDetail.AgeRange = BillingDetailRow.AgeRange;
                    BillingDetail.BillDeliveryCrStatusDate = BillingDetailRow.BillDeliveryCrStatusDate;
                    BillingDetail.BillingStatus = BillingDetailRow.BillingStatus;
                    BillingDetail.BillReceivedByCrStatus = BillingDetailRow.BillReceivedByCrStatus;
                    BillingDetail.BirthDate = BillingDetailRow.BirthDate;
                    BillingDetail.CollectionAgent = BillingDetailRow.CollectionAgent;
                    BillingDetail.CPEType = BillingDetailRow.CPEType;
                    BillingDetail.CSAFNumber = BillingDetailRow.CSAFNumber;
                    BillingDetail.CustomerNTN = BillingDetailRow.CustomerNTN;
                    BillingDetail.CustomerStr = BillingDetailRow.CustomerStr;
                    BillingDetail.InfinityMappedQubeePackageId = BillingDetailRow.InfinityMappedQubeePackageId;
                    BillingDetail.InfinityMigrationCategory = BillingDetailRow.InfinityMigrationCategory;
                    BillingDetail.InfinityRefAccNo = BillingDetailRow.InfinityRefAccNo;
                    BillingDetail.InfinityRefCPEType = BillingDetailRow.InfinityRefCPEType;
                    BillingDetail.InfinityRefMLR = BillingDetailRow.InfinityRefMLR;
                    BillingDetail.InfinityUnsuccessfulMigrationReason = BillingDetailRow.InfinityUnsuccessfulMigrationReason;
                    BillingDetail.InstallationDate = BillingDetailRow.InstallationDate;
                    BillingDetail.InstalledBy = BillingDetailRow.InstalledBy;
                    BillingDetail.IsDirectBilling = BillingDetailRow.IsDirectBilling;
                    BillingDetail.IsInitialPaymentReceived = BillingDetailRow.IsInitialPaymentReceived;
                    BillingDetail.IsPaperBillRequired = BillingDetailRow.IsPaperBillRequired;
                    BillingDetail.MGM_ExistingCustomerSng = BillingDetailRow.MGM_ExistingCustomerSng;
                    BillingDetail.NationalIdNo = BillingDetailRow.NationalIdNo;
                    BillingDetail.Package = BillingDetailRow.Package;
                    BillingDetail.PassportNo = BillingDetailRow.PassportNo;
                    BillingDetail.Reason = BillingDetailRow.Reason;
                    BillingDetail.RefAddress1 = BillingDetailRow.RefAddress1;
                    BillingDetail.RefAddress2 = BillingDetailRow.RefAddress2;
                    BillingDetail.RefCNIC1 = BillingDetailRow.RefCNIC1;
                    BillingDetail.RefCNIC2 = BillingDetailRow.RefCNIC2;
                    BillingDetail.RefContact1 = BillingDetailRow.RefContact1;
                    BillingDetail.RefContact2 = BillingDetailRow.RefContact2;
                    BillingDetail.RefEmail1 = BillingDetailRow.RefEmail1;
                    BillingDetail.RefEmail2 = BillingDetailRow.RefEmail2;
                    BillingDetail.RefName1 = BillingDetailRow.RefName1;
                    BillingDetail.RefName2 = BillingDetailRow.RefName2;
                    BillingDetail.RetailOutletID = BillingDetailRow.RetailOutletID;
                    BillingDetail.SalesAgentId = BillingDetailRow.SalesAgentId;
                    BillingDetail.SalesRepId = BillingDetailRow.SalesRepId;
                    BillingDetail.VerificationRemarks = BillingDetailRow.VerificationRemarks;
                    BillingDetail.VerificationStatus = BillingDetailRow.VerificationStatus;
                    BillingDetail.WifiRouter = BillingDetailRow.WifiRouter;
                    BillingDetail.UpdatedBy = request.UpdatedBy ;
                    BillingDetail.UpdatedAt = DateTime.Now;


                }
                else
                {
                    //add here
                    BillingDetail.AgeRange = BillingDetailRow.AgeRange;
                    BillingDetail.BillingName = BillingDetailRow.BillingName;
                    BillingDetail.BillDeliveryCrStatusDate = BillingDetailRow.BillDeliveryCrStatusDate;
                    BillingDetail.BillingStatus = BillingDetailRow.BillingStatus;
                    BillingDetail.BillReceivedByCrStatus = BillingDetailRow.BillReceivedByCrStatus;
                    BillingDetail.BirthDate = BillingDetailRow.BirthDate;
                    BillingDetail.CollectionAgent = BillingDetailRow.CollectionAgent;
                    BillingDetail.CPEType = BillingDetailRow.CPEType;
                    BillingDetail.CSAFNumber = BillingDetailRow.CSAFNumber;
                    BillingDetail.CustomerNTN = BillingDetailRow.CustomerNTN;
                    BillingDetail.CustomerStr = BillingDetailRow.CustomerStr;
                    BillingDetail.InfinityMappedQubeePackageId = BillingDetailRow.InfinityMappedQubeePackageId;
                    BillingDetail.InfinityMigrationCategory = BillingDetailRow.InfinityMigrationCategory;
                    BillingDetail.InfinityRefAccNo = BillingDetailRow.InfinityRefAccNo;
                    BillingDetail.InfinityRefCPEType = BillingDetailRow.InfinityRefCPEType;
                    BillingDetail.InfinityRefMLR = BillingDetailRow.InfinityRefMLR;
                    BillingDetail.InfinityUnsuccessfulMigrationReason = BillingDetailRow.InfinityUnsuccessfulMigrationReason;
                    BillingDetail.InstallationDate = BillingDetailRow.InstallationDate;
                    BillingDetail.InstalledBy = BillingDetailRow.InstalledBy;
                    BillingDetail.IsDirectBilling = BillingDetailRow.IsDirectBilling;
                    BillingDetail.IsInitialPaymentReceived = BillingDetailRow.IsInitialPaymentReceived;
                    BillingDetail.IsPaperBillRequired = BillingDetailRow.IsPaperBillRequired;
                    BillingDetail.MGM_ExistingCustomerSng = BillingDetailRow.MGM_ExistingCustomerSng;
                    BillingDetail.NationalIdNo = BillingDetailRow.NationalIdNo;
                    BillingDetail.Package = BillingDetailRow.Package;
                    BillingDetail.PassportNo = BillingDetailRow.PassportNo;
                    BillingDetail.Reason = BillingDetailRow.Reason;
                    BillingDetail.RefAddress1 = BillingDetailRow.RefAddress1;
                    BillingDetail.RefAddress2 = BillingDetailRow.RefAddress2;
                    BillingDetail.RefCNIC1 = BillingDetailRow.RefCNIC1;
                    BillingDetail.RefCNIC2 = BillingDetailRow.RefCNIC2;
                    BillingDetail.RefContact1 = BillingDetailRow.RefContact1;
                    BillingDetail.RefContact2 = BillingDetailRow.RefContact2;
                    BillingDetail.RefEmail1 = BillingDetailRow.RefEmail1;
                    BillingDetail.RefEmail2 = BillingDetailRow.RefEmail2;
                    BillingDetail.RefName1 = BillingDetailRow.RefName1;
                    BillingDetail.RefName2 = BillingDetailRow.RefName2;
                    BillingDetail.RetailOutletID = BillingDetailRow.RetailOutletID;
                    BillingDetail.SalesAgentId = BillingDetailRow.SalesAgentId;
                    BillingDetail.SalesRepId = BillingDetailRow.SalesRepId;
                    BillingDetail.VerificationRemarks = BillingDetailRow.VerificationRemarks;
                    BillingDetail.VerificationStatus = BillingDetailRow.VerificationStatus;
                    BillingDetail.WifiRouter = BillingDetailRow.WifiRouter;
                    BillingDetail.CreatedBy = request.UpdatedBy;
                    BillingDetail.CreatedAt = DateTime.Now;
                    BillingDetail.Date = DateTime.Now.Date;
                    BillingDetail.BillingInformationId = Billinginfoid;
                    var BillingDetailsResult = _dbContext.BillingDetails.Add(BillingDetail);
                    _dbContext.SaveChanges();
                }
                //BillingInfo id assingation
               
            }

            
            _dbContext.SaveChanges();
            return response;
        }
    }
}