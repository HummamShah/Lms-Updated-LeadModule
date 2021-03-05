using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanyAccounts
{

    public class GetCompanyAccountsResponse
    {
        public List<AccountEntriesData> Data { get; set; }
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxWitholding { get; set; }
    }

    public class AccountEntriesData
    {
        public int Id { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


    }

    public class GetCompanyAccountsRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
       public int Id { get; set; }
        public object RunRequest(GetCompanyAccountsRequest req)
        {
            var response = new GetCompanyAccountsResponse();
            response.Data = new List<AccountEntriesData>();
            var Data = _dbContext.Account.Where(x=>x.Id == req.Id).FirstOrDefault();
            response.Balance = Data.Balance;
            response.CompanyId = Data.CompanyId;
            response.CompanyName = Data.Company.Name;
            response.TaxWitholding = Data.TaxWitholding;
            foreach (var d in Data.AccountEntries)
            {
                var Account = new AccountEntriesData();
                Account.Id = d.Id;
                Account.Credit = d.Credit??0;
                Account.Debit = d.Debit ?? 0;
                Account.Description = d.Description;
                Account.CreatedAt = d.CreatedAt;
                Account.CreatedBy = d.CreatedBy;
                response.Data.Add(Account);
            }
            return response;

        }
    }
}