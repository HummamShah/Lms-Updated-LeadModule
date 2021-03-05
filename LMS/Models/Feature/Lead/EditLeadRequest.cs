using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
	public class EditLeadResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class EditLeadRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();

		public int Id { get; set; }
		public string UserId { get; set; }
		public int CompanyId { get; set; }
		public string ContactPersonName { get; set; }
		public string ContactPersonNumber { get; set; }
		public string ContactPersonEmail { get; set; }
		public string ContactPersonTitle { get; set; }
		public string ContactPersonDepartment { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public int? ParentCompanyId { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public int? Domain { get; set; }
		public int? MOCId { get; set; }
		public int? BOT { get; set; }
		public string CompanyAlternateContact { get; set; }
		public string Website { get; set; }
		public int? Area { get; set; }
		public int? City { get; set; }
		public int? NOE { get; set; } //Number of Employees may be change it to string
		public int? NoBracnhOffices { get; set; }  // need to add this field in database..
		public int? BusinessIndustry { get; set; }
		public string NTN { get; set; } // need to add this field in database..
		public string RequiredMedium { get; set; }
		public string FuturePlanOFExtention { get; set; } //not in db
		public int? CUDS { get; set; }
		public int? CUDSService { get; set; }
		public string CurrentlyUsedMedium { get; set; }
		public int NoLinks { get; set; }
		public decimal Budget { get; set; }
	
		public DateTime? EstimatedClosingDate { get; set; }
		public string Comments { get; set; }
		public bool IsEsisting { get; set; }
		public bool HasTriedOurServie { get; set; }
		public int ConnectivityType { get; set; }
		public string Bandwidth { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public object RunRequest(EditLeadRequest request)
		{
			var response = new EditLeadResponse();
			var AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
			var Lead =_dbContext.Lead.Where(x => x.Id == request.Id).FirstOrDefault();
			//Lead.AgentId = AgentId;
			Lead.CompanyId = request.CompanyId;
			Lead.UpdatedAt = DateTime.Today;
			Lead.UpdatedBy = request.UpdatedBy;
			Lead.Name = request.Name;
			Lead.Address = request.Address;
			Lead.Email = request.Email;
			Lead.ModeOfCommunication = request.MOCId;
			Lead.BusinessOperationTime = request.BOT;
			Lead.AlternateNumber = request.CompanyAlternateContact;
			Lead.Contact = request.Contact;
			Lead.ContactPersonName = request.ContactPersonName;
			Lead.ContactPersonNumber = request.ContactPersonNumber;
			Lead.ContactPersonEmail = request.ContactPersonEmail;
			Lead.ContactPersonTitle = request.ContactPersonTitle;
			Lead.ContactPersonDepartment = request.ContactPersonDepartment;
			Lead.Domain = request.Domain;
			Lead.Website = request.Website;
			Lead.Area = request.Area;
			Lead.City = request.City;
			Lead.NoEmployee = request.NOE;
			Lead.BusinessIndustry = request.BusinessIndustry;
			Lead.NTN = request.NTN;
			Lead.NumberOfBranchOffices = request.NoBracnhOffices;
			Lead.RequiredMedium = request.RequiredMedium;
			Lead.CurrentlyUsedMedium = request.CurrentlyUsedMedium;
			
			Lead.NoLinks = request.NoLinks;
			Lead.Budget = request.Budget;
			Lead.EstimatedClosingDate = request.EstimatedClosingDate;
			Lead.IsEsisting = request.IsEsisting;
			Lead.HasTriedOurServie = request.HasTriedOurServie;
			Lead.Comments = request.Comments;
			Lead.Bandwidth = request.Bandwidth;
			Lead.ConnectivityType = request.ConnectivityType;
			Lead.LeadStatus = (int)LeadStatus.Open;
			Lead.PmdStatus = (int)PmdStatus.None;
			Lead.QuotationStatus = (int)QuotationStatus.None;
			_dbContext.SaveChanges();
			return response;
		}
	}
}