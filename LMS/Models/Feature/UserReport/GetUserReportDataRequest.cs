using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.UserReport
{
    public class GetUserReportDataRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

        public string UserId { get; set; }
        
        public Object RunRequest(GetUserReportDataRequest req)
        {
            var response = new GetUserReportDataResponse();
            response.TotalLeadsData = new AgentsTotalLeadsData();
            response.MonthlyLeadsData = new AgentsMonthlyLeadsData();
            response.MonthlyLeadsData.MonthlyOpenLeadsData = new List<int>();
            response.MonthlyLeadsData.MonthlyCompletedLeadsData = new List<int>();
            response.MonthlyLeadsData.MonthlyCancelledLeadsData = new List<int>();
            var Leads = _dbContext.Lead.ToList();
           
            if (req.UserId != null && req.UserId != string.Empty)
            {
                var Agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
                if (Agent != null)
                {
                    var AgentId = Agent.Id;
                    var SuperVisorId = Agent.SuperVisorId;
                    Leads = Leads.Where(x => x.AssignedPmdId == AgentId || x.AssignedPreSaleId == AgentId || x.AssignedToId == AgentId || x.AgentId == AgentId).ToList();
                }

            }
            response.TotalLeadsData.TotalCountOfLeads = Leads.Count();
            response.TotalLeadsData.TotalCancelledLeads = Leads.Where(x=>x.LeadStatus == (int)LeadStatus.Cancelled).Count();
            response.TotalLeadsData.TotalCompletedLeads = Leads.Where(x => x.LeadStatus == (int)LeadStatus.Completed).Count();
            response.TotalLeadsData.TotalOpenLeads = Leads.Where(x => x.LeadStatus == (int)LeadStatus.Open).Count();
            response.TotalLeadsData.TotalFeasibleLeads = Leads.Where(x => x.PmdStatus == (int)PmdStatus.Feasible).Count();
            response.TotalLeadsData.TotalNotFeasibleLeads = Leads.Where(x => x.PmdStatus == (int)PmdStatus.NotFeasible).Count();
            response.TotalLeadsData.TotalPendingByPMDLeads = Leads.Where(x => x.PmdStatus == (int)PmdStatus.None).Count();
            response.TotalLeadsData.TotalApprovedLeads = Leads.Where(x =>x.IsApproved==true).Count();
            response.TotalLeadsData.TotalNotApprovedLeads = Leads.Where(x => x.IsApproved == false).Count();
            response.TotalLeadsData.TotalPendingByAdminLeads = Leads.Where(x => x.IsApproved == null).Count();


            for (var i =1 ; i<=12; i++)
            {
                DateTime now = DateTime.Now;
                var MonthsStartDate = new DateTime(now.Year, i, 1);
                var MonthsEndDate = MonthsStartDate.AddMonths(1).AddDays(-1);
                var ThisMonthsCancelledLeads = Leads.Where(x => x.Date <= MonthsEndDate && x.Date >= MonthsStartDate && x.LeadStatus == (int)LeadStatus.Cancelled).Count();
                response.MonthlyLeadsData.MonthlyCancelledLeadsData.Add(ThisMonthsCancelledLeads);

                var ThisMonthsOpenLeads = Leads.Where(x => x.Date <= MonthsEndDate && x.Date >= MonthsStartDate && x.LeadStatus == (int)LeadStatus.Open).Count();
                response.MonthlyLeadsData.MonthlyOpenLeadsData.Add(ThisMonthsOpenLeads);

                var ThisMonthsCompletedLeads = Leads.Where(x => x.Date <= MonthsEndDate && x.Date >= MonthsStartDate && x.LeadStatus == (int)LeadStatus.Completed).Count();
                response.MonthlyLeadsData.MonthlyCompletedLeadsData.Add(ThisMonthsCompletedLeads);

            }
            return response;

        }
        public class AgentsTotalLeadsData
        {
            public int TotalCountOfLeads { get; set; }
            public int TotalOpenLeads {get;set;}
            public int TotalCompletedLeads { get; set; }
            public int TotalCancelledLeads { get; set; }
            public int TotalFeasibleLeads { get; set; }
            public int TotalNotFeasibleLeads { get; set; }
            public int TotalPendingByPMDLeads { get; set; }
            public int TotalPendingByAdminLeads { get; set; }
            public int TotalApprovedLeads { get; set; }
            public int TotalNotApprovedLeads { get; set; }

        }
        public class AgentsMonthlyLeadsData
        {
            public List<int> MonthlyOpenLeadsData { get; set; }
            public List<int> MonthlyCompletedLeadsData { get; set; }
            public List<int> MonthlyCancelledLeadsData { get; set; }
        }
        public class GetUserReportDataResponse
        {
            public AgentsTotalLeadsData TotalLeadsData { get; set; }
            public AgentsMonthlyLeadsData MonthlyLeadsData { get; set; }
        }
    }
}