using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanyAccounts
{
    public class GetListingResponse
    {
        public List<AccountsData> Data { get; set; }
    }

    public class AccountsData
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? TaxWitholding { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


    }

    public class GetListingRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int Type { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetListingRequest req)
        {
            var response = new GetListingResponse();
            response.Data = new List<AccountsData>();
            var Data = _dbContext.Account;

            foreach (var d in Data)
            {
                var Account = new AccountsData();
                Account.Id = d.Id;
                
                Account.Balance = d.Balance;
                Account.CompanyId = d.CompanyId;
                Account.CompanyName = d.Company.Name;
                Account.TaxWitholding = d.TaxWitholding;
                Account.CreatedAt = d.CreatedAt;
                Account.CreatedBy = d.CreatedBy;
                response.Data.Add(Account);
            }
            return response;

        }
    }
}