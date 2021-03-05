using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
    
    public class GetAccountByCompanyIdResponse
    {
        public AccountData Account { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AccountData
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string CompanyEmail { get; set; }
        public string NTN { get; set; }
    }
    public class GetAccountByCompanyIdRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int CompanyId { get; set; }
        public object RunRequest(GetAccountByCompanyIdRequest request)
        {
            var response = new GetAccountByCompanyIdResponse();
            var Company = _dbContext.Company.Where(x => x.Id == request.CompanyId).FirstOrDefault();
            if (Company.Account.Count > 0 )
            {
                var CompanyAccount = Company.Account.FirstOrDefault();
                var AccountData = new AccountData();
                AccountData.AccountId = CompanyAccount.Id;
                AccountData.AccountName = CompanyAccount.Name;
                AccountData.CompanyAddress = CompanyAccount.Company.Address;
                AccountData.CompanyContact = CompanyAccount.Company.Contact;
                AccountData.CompanyEmail = CompanyAccount.Company.Email;
                AccountData.NTN = CompanyAccount.Company.NTN;
                response.Account = AccountData;
            }
            return response;
        }
    }
}