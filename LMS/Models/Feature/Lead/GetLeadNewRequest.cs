using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{

	public class CompanyDetail
	{

		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int? MOCId { get; set; }
		public int? BOT { get; set; }
		public string CompanyAlternateContact { get; set; }
		public string Website { get; set; }
		public int? Area { get; set; }
		public int? City { get; set; }
		public int? NOE { get; set; } //Number of Employees may be change it to string

		public int? BusinessIndustry { get; set; }
		public string NTN { get; set; } // need to add this field in database..

		public string FuturePlanOFExtention { get; set; } //not in db
		public int? CUDS { get; set; }
		public int? CUDSService { get; set; }

		public int NoLinks { get; set; }
		
		public string CityEnum { get; set; }

		public int? NoBracnhOffices { get; set; }

		public string DomainName { get; set; }
	
		public string MOCName { get; set; }

		public string BOTEnum { get; set; }
	}
	public class SolutionDetail
	{
		public int Id { get; set; }
		public int LeadId { get; set; }
		public DateTime? Date { get; set; }
		public int SolutionType { get; set; }
		public int? SolutionSubType { get; set; }
		public string SolutionServiceProvider { get; set; }
		public string SolutionServiceProduct { get; set; }
		public bool? IsNew { get; set; }
		public string CurrentServiceInfo { get; set; }
		public int? Duration { get; set; }
		public string OtherMeasurements { get; set; }
		public int? Quantity { get; set; }
		public string Remarks { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
	public class ConnectivityDetail
	{
		public int Id { get; set; }
		public string CurrentlyUsedMedium { get; set; }
		public string RequiredMedium { get; set; }
		public int? ConnectivityType { get; set; }
		public string ConnectivityTypeEnum { get; set; }
		public decimal? Budget { get; set; }
		public DateTime? EstimatedClosingDate { get; set; }
		public int? NoBracnhOffices { get; set; }  // need to add this field in database..
		public string Bandwidth { get; set; }
		public string Comments { get; set; }
		public bool? IsEsisting { get; set; }
		public bool? HasTriedOurServie { get; set; }
	}
	public class GetLeadNewRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }

		public object RunRequest(GetLeadNewRequest req)
		{
			var response = new GetLeadNewResponse();
			response.SolutionDetails = new SolutionDetail();
			response.CompanyDetails = new CompanyDetail();
			response.ConnectivityDetails = new ConnectivityDetail();
			var Data = _dbContext.Lead.Where(x => x.Id == req.Id).FirstOrDefault();
			response.Id = Data.Id;
			response.Name = Data.Name;
			response.Quotation = Data.Quotation;
			response.CompanyId = Data.CompanyId.Value;
			response.QuotationMrc = Data.QuotationMRC;
			response.QuotationOtc = Data.QuotationOTC;
			response.FeasibilityDetails = new List<FeasibilityDetails>();
			if (Data.CompanyId.HasValue)
			{
				response.CompanyName = Data.Company.Name;
			}
			response.Description = Data.Description;
		
			response.CreatedAt = Data.CreatedAt;
			response.CreatedBy = Data.CreatedBy;
			response.UpdatedAt = Data.UpdatedAt;
			response.UpdatedBy = Data.UpdatedBy;
	
			response.CompanyAlternateContact = Data.AlternateNumber;
			if (Data.AssignedToId.HasValue)
			{
				response.AssignedToName = Data.AssignedTo.FisrtName + Data.AssignedTo.LastName;
				response.AssignedToId = Data.AssignedToId;
				response.AssignedOn = Data.LeadAssignedOn;

			}

			if (Data.AssignedPmdId.HasValue)
			{
				response.AssingnedPmdName = Data.AssignedPMD.FisrtName + " " + Data.AssignedPMD.LastName;
				response.AssignedPmdId = Data.AssignedPmdId;
				response.PmdAssignedOn = Data.PmdAssignedOn;


			}
			if (Data.AssignedPreSaleId.HasValue)
			{
				response.AssignedPreSaleName = Data.AssignedPreSale.FisrtName + " " + Data.AssignedPreSale.LastName;
				response.AssignedPreSaleId = Data.AssignedPreSaleId;
				response.PreSaleAssignedOn = Data.PresaleAssignedOn;

			}
			if (Data.Domain != null)
			{
				response.Domain = Data.Domain;
				response.DomainName = ((Domain)Data.Domain.Value).ToString();
			}
			

			if (Data.LeadStatus.HasValue)
			{
				response.LeadStatus = Data.LeadStatus.Value;
				response.LeadStatusEnum = ((LeadStatus)Data.LeadStatus.Value).ToString();
			}
			if (Data.PmdStatus.HasValue)
			{
				response.PmdStatus = Data.PmdStatus.Value;
				response.PmdStatusEnum = ((PmdStatus)Data.PmdStatus.Value).ToString();
			}
			if (Data.QuotationStatus.HasValue)
			{
				response.QuotationStatus = Data.QuotationStatus.Value;
				response.QuotationStatusEnum = ((QuotationStatus)Data.QuotationStatus.Value).ToString();
			}

            if (Data.Domain == (int)Domain.Connectivity)
			{
				response.ConnectivityDetails.RequiredMedium = Data.RequiredMedium;
				response.ConnectivityDetails.CurrentlyUsedMedium = Data.CurrentlyUsedMedium;
				response.ConnectivityDetails.IsEsisting = Data.IsEsisting;
				response.ConnectivityDetails.HasTriedOurServie = Data.HasTriedOurServie;
				response.ConnectivityDetails.Budget = Data.Budget;
				response.ConnectivityDetails.EstimatedClosingDate = Data.EstimatedClosingDate;
				response.ConnectivityDetails.Comments = Data.Comments;
				response.ConnectivityDetails.ConnectivityType = Data.ConnectivityType;
				if (Data.ConnectivityType.HasValue)
				{
					response.ConnectivityDetails.ConnectivityTypeEnum = ((ConnectionType)Data.ConnectivityType.Value).ToString();
				}

				response.ConnectivityDetails.Bandwidth = Data.Bandwidth;
			}
			if (Data.Domain == (int)Domain.SolutionSet)
			{
				if(Data.SolutionDetails.Count > 0)
                {
					var SolutionSet = Data.SolutionDetails.FirstOrDefault();
					response.SolutionDetails.Id = SolutionSet.Id;
					response.SolutionDetails.LeadId = SolutionSet.LeadId;
					response.SolutionDetails.IsNew= SolutionSet.IsNew;
					response.SolutionDetails.OtherMeasurements=SolutionSet.OtherMeasurements  ;
					response.SolutionDetails.Quantity = SolutionSet.Quantity;
					response.SolutionDetails.Remarks=SolutionSet.Remarks;
					response.SolutionDetails.SolutionServiceProduct  = SolutionSet.SolutionServiceProduct ;
					response.SolutionDetails.SolutionServiceProvider = SolutionSet.SolutionServiceProvider;
					response.SolutionDetails.SolutionSubType=SolutionSet.SolutionSubType;
					response.SolutionDetails.SolutionType=SolutionSet.SolutionType;
					response.SolutionDetails.CurrentServiceInfo = SolutionSet.CurrentServiceInfo;
					response.SolutionDetails.Duration = SolutionSet.Duration;
					response.SolutionDetails.CreatedAt= SolutionSet.CreatedAt;
					response.SolutionDetails.Date=SolutionSet.Date;
					response.SolutionDetails.CreatedBy = SolutionSet.CreatedBy;
					
				}
			
			}

			response.IsApproved = Data.IsApproved;
			response.AdminRemarks = Data.AdminRemarks;
			response.ContactPersonName = Data.ContactPersonName;
			response.ContactPersonNumber = Data.ContactPersonNumber;
			response.ContactPersonEmail = Data.ContactPersonEmail;
			response.ContactPersonTitle = Data.ContactPersonTitle;
			response.ContactPersonDepartment = Data.ContactPersonDepartment;
			
	
			response.NoLinks = Data.NoLinks;
			//Company Details
			response.CompanyDetails.NoBracnhOffices = Data.NumberOfBranchOffices;
			response.CompanyDetails.Website = Data.Website;
			response.CompanyDetails.NTN = Data.NTN;
			response.CompanyDetails.Address = Data.Address;
			response.CompanyDetails.Contact = Data.Contact;
			response.CompanyDetails.Email = Data.Email;
			response.CompanyDetails.CompanyAlternateContact = Data.AlternateNumber;
			response.QuotationRemarks = Data.QuotationRemarks;
			if (Data.ModeOfCommunication != null)
			{
				response.CompanyDetails.MOCId = Data.ModeOfCommunication;
				response.CompanyDetails.MOCName = ((ModeOfCommunication)Data.ModeOfCommunication.Value).ToString();
			}
			if (Data.BusinessOperationTime.HasValue)
			{
				response.CompanyDetails.BOTEnum = ((BusinessOperationTime)Data.BusinessOperationTime.Value).ToString();
				response.CompanyDetails.BOT = Data.BusinessOperationTime;
			}
			if (Data.City.HasValue)
			{
				response.CompanyDetails.City = Data.City;
				response.CompanyDetails.CityEnum = ((City)Data.City.Value).ToString();
			}
			if (Data.Company != null) // need to remove this once all the data is filled with company
			{
				response.CompanyDetails.Latitude = Data.Company.Latitude ?? 0;
				response.CompanyDetails.Longitude = Data.Company.Longitude ?? 0;

			}
			foreach (var feasibility in Data.PmdDetails)
			{
				var FeasibilityRow = new FeasibilityDetails();
				FeasibilityRow.Bandwidth = feasibility.Bandwidth;
				FeasibilityRow.CreatedAt = feasibility.CreatedAt;
				FeasibilityRow.CreatedBy = feasibility.CreatedBy;
				FeasibilityRow.Id = feasibility.Id;
				FeasibilityRow.LeadId = feasibility.LeadId;
				FeasibilityRow.MRC = feasibility.MRC;
				FeasibilityRow.OTC = feasibility.OTC;
				FeasibilityRow.Remarks = feasibility.Remarks;
				FeasibilityRow.VendorId = feasibility.VendorId;
				FeasibilityRow.VendorName = feasibility.Vendor.Name;
				FeasibilityRow.ConnectivityType = feasibility.ConnectivityType;
				FeasibilityRow.ConnectivityTypeEnum = ((ConnectivityType)feasibility.ConnectivityType).ToString();
				response.FeasibilityDetails.Add(FeasibilityRow);
			}
			return response;
		}

		public class GetLeadNewResponse
		{
			public decimal? Quotation { get; set; }
			public decimal QuotationMrc { get; set; }
			public decimal QuotationOtc { get; set; }
			public int Id { get; set; }
			public int CompanyId { get; set; }
			public string CompanyName { get; set; }
			public CompanyDetail CompanyDetails { get; set; }
			public SolutionDetail SolutionDetails { get; set; }
			public ConnectivityDetail ConnectivityDetails { get; set; }
			public string QuotationRemarks { get; set; }
			public string Name { get; set; }
			public string ContactPersonName { get; set; }
			public string CompanyAlternateContact { get; set; }
			public string ContactPersonNumber { get; set; }
			public string ContactPersonEmail { get; set; }
			public string ContactPersonTitle { get; set; }
			public string ContactPersonDepartment { get; set; }
			public string Description { get; set; }
			
			public int? Domain { get; set; }
			public string DomainName { get; set; }


		
	
		
			public int? NOE { get; set; } //Number of Employees may be change it to string
			public string NOEEnum { get; set; }
			public string RequiredMedium { get; set; }
			public int? BusinessIndustry { get; set; }
			public string BusinessIndustryEnum { get; set; }

	
			public int? NoLinks { get; set; }
			public int? AssignedToId { get; set; }
			public string AssignedToName { get; set; }
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
			public string AdminRemarks { get; set; }
			public DateTime? PmdAssignedOn { get; set; }
			public DateTime? PreSaleAssignedOn { get; set; }
			public int? Area { get; set; }
			public string AreaEnum { get; set; }


	

			public string CreatedBy { get; set; }
			public DateTime? CreatedAt { get; set; }
			public string UpdatedBy { get; set; }
			public DateTime? UpdatedAt { get; set; }
			public List<FeasibilityDetails> FeasibilityDetails { get; set; }

		}
		public class FeasibilityDetails
		{
			public int Id { get; set; }
			public int? LeadId { get; set; }
			public string Bandwidth { get; set; }
			public decimal? OTC { get; set; }
			public decimal? MRC { get; set; }
			public int? VendorId { get; set; }
			public string VendorName { get; set; }
			public string Remarks { get; set; }
			public int ConnectivityType { get; set; }
			public string ConnectivityTypeEnum { get; set; }
			public string CreatedBy { get; set; }
			public DateTime? CreatedAt { get; set; }
		}
	}
}