using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.CompanyAccounts
{

    public class GetChildListDataResponse
    {
        public List<AccountsData> Data { get; set; }
    }
    public class GetChildListRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        //Pagination Fields future task
        public int Id { get; set; }
        public int Type { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public object RunRequest(GetChildListRequest req)
        {
            var response = new GetChildListDataResponse();
            response.Data = new List<AccountsData>();
            var Data = _dbContext.Account.Where(x =>x.Company.ParentCompanyId == req.Id && x.Company.IsBranch == true);

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