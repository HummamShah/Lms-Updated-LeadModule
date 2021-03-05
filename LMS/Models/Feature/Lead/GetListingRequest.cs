using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    
	public class GetListingResponse
	{
		public List<LeadData> Data { get; set; }
		public int TotalRecords { get; set; }
	}
	public class LeadData
	{
	
		public int Id { get; set; }
		public bool IsFeasibilityAdded { get; set; }
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public int? Domain { get; set; }
		public string DomainName { get; set; }
		public int? MOCId { get; set; }
		public string MOCName { get; set; }
		public decimal? Budget { get; set; }
		public DateTime? EstimatedClosingDate { get; set; }
		public string Comments { get; set; }

		public int? AssignedToId { get; set; }
		public string AssignedToName {get;set;}
		public DateTime? AssignedOn { get; set; }
		public int? AssignedPmdId { get; set; }
		public string AssingnedPmdName { get; set; }
		public int? AssignedPreSaleId { get; set; }
		public string AssignedPreSaleName { get; set; }

		public int LeadStatus { get; set; }
		public string LeadStatusEnum { get; set; }
		public int? PmdStatus { get; set; }
		public string PmdStatusEnum { get; set; }
		public int QuotationStatus { get; set; }
		public string QuotationStatusEnum { get; set; }
		public bool? IsApproved { get; set; }
		


		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }


	}
	public class GetListingRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public string UserId { get; set; }
		//Pagination Fields
		public int CurrentPage { get; set; }
		public int PageSize{ get; set; }
		

		public object RunRequest(GetListingRequest req)
		{
			var response = new GetListingResponse();
			response.Data = new List<LeadData>();
			var SkippingNumber = req.PageSize * (req.CurrentPage - 1);
			var Data = _dbContext.Lead.ToList();
			
			if (req.UserId != null && req.UserId != string.Empty)
			{
				var Agent = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
				if (Agent != null)
				{
					var AgentId = Agent.Id;
					var SuperVisorId = Agent.SuperVisorId;
					var juniors = _dbContext.Agent.Where(x => x.SuperVisorId == AgentId);
                   
					foreach (var junior in juniors)
                    {
						Data = Data.Where(x => x.AgentId == AgentId || x.AssignedToId == AgentId || x.AgentId == junior.Id ).ToList();
						
					}
					//|| x.AgentId == SuperVisorId
					if (juniors.Count() <= 0) // if there are no juniors
					{
						Data = Data.Where(x => x.AgentId == AgentId || x.AssignedToId == AgentId ).ToList();
					}


				}
				
            }
			response.TotalRecords = Data.Count();
			Data = Data.Skip(SkippingNumber).Take(req.PageSize).ToList();
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
					temp.AssignedToName = d.AssignedTo.FisrtName + " " + d.AssignedTo.LastName;
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
					temp.LeadStatusEnum=((LeadStatus)d.LeadStatus.Value).ToString();
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
				response.Data.Add(temp);
			}
			return response;
		}
	}
}