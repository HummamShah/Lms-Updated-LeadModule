using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    

	public class GetListingForPmdResponse
	{
		public List<LeadData> Data { get; set; }
	}
	
	public class GetListingForPmdRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public string UserId { get; set; }
		public object RunRequest(GetListingForPmdRequest req)
		{
			var response = new GetListingForPmdResponse();
			response.Data = new List<LeadData>();
			var Data = _dbContext.Lead.ToList();
			if (req.UserId != null && req.UserId != string.Empty)
			{
				var Agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
				if (Agent != null)
				{
					var AgentId = Agent.Id;
					var SuperVisorId = Agent.SuperVisorId;
					Data = Data.Where(x => x.AssignedPmdId == AgentId || x.AssignedPreSaleId == AgentId).ToList();
				}

			}

			foreach (var d in Data)
			{
				var temp = new LeadData();
				temp.Id = d.Id;
				temp.Name = d.Name;
				temp.CompanyId = d.CompanyId.Value;
				if (d.CompanyId.HasValue)
				{
					temp.CompanyName = d.Company.Name;
				}
				temp.Description = d.Description;
				temp.Address = d.Address;
				temp.Contact = d.Contact;
				temp.Email = d.Email;
				temp.CreatedAt = d.CreatedAt;
				temp.CreatedBy = d.CreatedBy;
				temp.UpdatedAt = d.UpdatedAt;
				temp.UpdatedBy = d.UpdatedBy;
				temp.Budget = d.Budget;
				temp.EstimatedClosingDate = d.EstimatedClosingDate;
				temp.Comments = d.Comments;

				if (d.AssignedToId.HasValue)
				{
					temp.AssignedToName = d.AssignedTo.FisrtName + " "  +  d.AssignedTo.LastName;
					temp.AssignedToId = d.AssignedToId;
					temp.AssignedOn = d.LeadAssignedOn;

				}

				if (d.AssignedPmdId.HasValue)
				{
					temp.AssingnedPmdName = d.AssignedPMD.FisrtName + " " + d.AssignedPMD.LastName;
					temp.AssignedPmdId = d.AssignedPmdId;


				}
				if (d.AssignedPreSaleId.HasValue)
				{
					temp.AssignedPreSaleName = d.AssignedPreSale.FisrtName + " " + d.AssignedPreSale.LastName;
					temp.AssignedPreSaleId = d.AssignedPreSaleId;


				}
				if (d.Domain != null)
				{
					temp.Domain = d.Domain;
					temp.DomainName = ((Domain)d.Domain.Value).ToString();
				}
				if (d.ModeOfCommunication != null)
				{
					temp.MOCId = d.ModeOfCommunication;
					temp.MOCName = ((ModeOfCommunication)d.ModeOfCommunication.Value).ToString();
				}
				if (d.LeadStatus.HasValue)
				{
					temp.LeadStatus = d.LeadStatus.Value;
					temp.LeadStatusEnum = ((LeadStatus)d.LeadStatus.Value).ToString();
				}
				if (d.PmdStatus.HasValue)
				{
					temp.PmdStatus = d.PmdStatus.Value;
					temp.PmdStatusEnum = ((PmdStatus)d.PmdStatus.Value).ToString();
				}
				if (d.QuotationStatus.HasValue)
				{
					temp.QuotationStatus = d.QuotationStatus.Value;
					temp.QuotationStatusEnum = ((QuotationStatus)d.QuotationStatus.Value).ToString();
				}

				temp.IsApproved = d.IsApproved;

				if (d.PmdDetails.Count > 0)
				{
					temp.IsFeasibilityAdded = true;

				}
				response.Data.Add(temp);
			}
			return response;
		}
	}
}