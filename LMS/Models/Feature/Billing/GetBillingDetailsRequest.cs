using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Billing
{
    public class GetBillingDetailsRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int AccountId { get; set; }
        public object RunRequest(GetBillingDetailsRequest req)
        {
            var resp = new GetBillingDetailsResponse();
            resp.Billings = new List<BDetails>();
           var Account = _dbContext.Account.Where(x=>x.Id== req.AccountId).FirstOrDefault();
            var BillingInfo = Account.Company.BillingInformation;
            foreach (var row in Account.Company.BillingInformation.BillingDetails)
            {
                var detail = new BDetails();
                detail.AccountId = Account.Id;
                detail.BillingName = row.BillingName;
                detail.CompanyAddress = BillingInfo.Address1;
                detail.Id = row.Id;
                detail.NTN = row.CustomerNTN;
                detail.PackageId = row.Package;
                detail.POCContact = BillingInfo.POCNumber;
                detail.POCName = BillingInfo.POCName;
                detail.POCEmail = BillingInfo.CompanyContact;
                resp.Billings.Add(detail);

            }
            return resp;
        }
    }
    public class GetBillingDetailsResponse
    {
        public List<BDetails> Billings { get; set; }
        public List<string> ValidationErrors { get; set; }

    }
    public class BDetails
    {
        public int Id { get; set; }
        public string BillingName { get; set; }
        public string NTN { get; set; }
        public string PackageId { get; set; }
        public string POCName { get; set; }
        public string CompanyAddress { get; set; }
        public string POCEmail { get; set; }
        public string POCContact { get; set; }
        public int AccountId { get; set; }
    }
}