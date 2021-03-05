using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
	public class EditNewLeadResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}

	public class EditNewLeadRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public CompanyDetails CompanyDetails { get; set; }
		public SolutionDetails SolutionDetails { get; set; }
		public ConnectivityDetails ConnectivityDetails { get; set; }
		public string ContactPersonName { get; set; }
		public string ContactPersonNumber { get; set; }
		public string ContactPersonEmail { get; set; }
		public string ContactPersonTitle { get; set; }
		public string ContactPersonDepartment { get; set; }
		public int? Domain { get; set; }
		public string UserId { get; set; }
		public int CompanyId { get; set; }
		public string UpdatedBy { get; set; }

		public object RunRequest(EditNewLeadRequest request)
		{
			var response = new EditNewLeadResponse();
			var AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
			var Lead = _dbContext.Lead.Where(x => x.Id == request.Id).FirstOrDefault();
			Lead.AgentId = AgentId;
			Lead.CompanyId = request.CompanyId;
			Lead.UpdatedAt = DateTime.Today;
			Lead.UpdatedBy = request.UpdatedBy;
			//Lead.Name = request.Name;

			Lead.ContactPersonName = request.ContactPersonName;
			Lead.ContactPersonNumber = request.ContactPersonNumber;
			Lead.ContactPersonEmail = request.ContactPersonEmail;
			Lead.ContactPersonTitle = request.ContactPersonTitle;
			Lead.ContactPersonDepartment = request.ContactPersonDepartment;
			Lead.Domain = request.Domain;
			//CompanyDetails
			Lead.Address = request.CompanyDetails.Address;
			Lead.Email = request.CompanyDetails.Email;
			Lead.ModeOfCommunication = request.CompanyDetails.MOCId;
			Lead.BusinessOperationTime = request.CompanyDetails.BOT;
			Lead.AlternateNumber = request.CompanyDetails.CompanyAlternateContact;
			Lead.Contact = request.CompanyDetails.Contact;
			Lead.Website = request.CompanyDetails.Website;
			Lead.Area = request.CompanyDetails.Area;
			Lead.City = request.CompanyDetails.City;
			Lead.NoEmployee = request.CompanyDetails.NOE;
			Lead.NoLinks = request.CompanyDetails.NoLinks;
			Lead.BusinessIndustry = request.CompanyDetails.BusinessIndustry;
			Lead.NTN = request.CompanyDetails.NTN;
			

			if (request.Domain == (int)LMS.Models.Enums.Domain.Connectivity)
			{
				//ConnectivityDetails
				Lead.NumberOfBranchOffices = request.ConnectivityDetails.NoBracnhOffices;
				Lead.RequiredMedium = request.ConnectivityDetails.RequiredMedium;
				Lead.CurrentlyUsedMedium = request.ConnectivityDetails.CurrentlyUsedMedium;
				Lead.Budget = request.ConnectivityDetails.Budget;
				Lead.EstimatedClosingDate = request.ConnectivityDetails.EstimatedClosingDate;
				Lead.IsEsisting = request.ConnectivityDetails.IsEsisting;
				Lead.HasTriedOurServie = request.ConnectivityDetails.HasTriedOurServie;
				Lead.Comments = request.ConnectivityDetails.Comments;
				Lead.Bandwidth = request.ConnectivityDetails.Bandwidth;
				Lead.ConnectivityType = request.ConnectivityDetails.ConnectivityType;
			}
			if (request.Domain == (int)LMS.Models.Enums.Domain.SolutionSet)
			{
				var SolutionSet = new LMS.Models.EntityModel.SolutionDetails();
                if (request.SolutionDetails.Id > 0)
                {
					SolutionSet = _dbContext.SolutionDetails.Where(x => x.Id == request.SolutionDetails.Id).FirstOrDefault();
					SolutionSet.LeadId = request.SolutionDetails.LeadId;
					SolutionSet.IsNew = request.SolutionDetails.IsNew;
					SolutionSet.OtherMeasurements = request.SolutionDetails.OtherMeasurements;
					SolutionSet.Quantity = request.SolutionDetails.Quantity;
					SolutionSet.Remarks = request.SolutionDetails.Remarks;
					SolutionSet.SolutionServiceProduct = request.SolutionDetails.SolutionServiceProduct;
					SolutionSet.SolutionServiceProvider = request.SolutionDetails.SolutionServiceProvider;
					SolutionSet.SolutionSubType = request.SolutionDetails.SolutionSubType;
					SolutionSet.SolutionType = request.SolutionDetails.SolutionType;
					SolutionSet.CurrentServiceInfo = request.SolutionDetails.CurrentServiceInfo;
					SolutionSet.Duration = request.SolutionDetails.Duration;
					SolutionSet.UpdatedAt = DateTime.Now;
					SolutionSet.UpdatedBy = request.UpdatedBy;
                }
                else
                {
					SolutionSet.LeadId = request.SolutionDetails.LeadId;
					SolutionSet.IsNew = request.SolutionDetails.IsNew;
					SolutionSet.OtherMeasurements = request.SolutionDetails.OtherMeasurements;
					SolutionSet.Quantity = request.SolutionDetails.Quantity;
					SolutionSet.Remarks = request.SolutionDetails.Remarks;
					SolutionSet.SolutionServiceProduct = request.SolutionDetails.SolutionServiceProduct;
					SolutionSet.SolutionServiceProvider = request.SolutionDetails.SolutionServiceProvider;
					SolutionSet.SolutionSubType = request.SolutionDetails.SolutionSubType;
					SolutionSet.SolutionType = request.SolutionDetails.SolutionType;
					SolutionSet.CurrentServiceInfo = request.SolutionDetails.CurrentServiceInfo;
					SolutionSet.Duration = request.SolutionDetails.Duration;
					SolutionSet.CreatedAt = DateTime.Now;
					SolutionSet.Date = DateTime.Now.Date;
					SolutionSet.CreatedBy = request.UpdatedBy;
					var SolutionResult = _dbContext.SolutionDetails.Add(SolutionSet);
				}
				_dbContext.SaveChanges();
			}


			Lead.LeadStatus = (int)LeadStatus.Open;
			Lead.PmdStatus = (int)PmdStatus.None;
			Lead.QuotationStatus = (int)QuotationStatus.None;
			_dbContext.SaveChanges();
			return response;
		}
	}
}