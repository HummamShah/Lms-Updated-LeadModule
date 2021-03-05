using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{


	public class AddLeadNewResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class CompanyDetails
    {
	
		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
	
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
	}
	public class SolutionDetails
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
		public int Duration { get; set; }
		public string OtherMeasurements { get; set; }
		public int? Quantity { get; set; }
		public string Remarks { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
	public class ConnectivityDetails
    {
		public int Id { get; set; }
		public string CurrentlyUsedMedium { get; set; }
		public string RequiredMedium { get; set; }
		public int ConnectivityType { get; set; }
		public decimal Budget { get; set; }
		public DateTime? EstimatedClosingDate { get; set; }
		public int? NoBracnhOffices { get; set; }  // need to add this field in database..
		public string Bandwidth { get; set; }
		public string Comments { get; set; }
		public bool IsEsisting { get; set; }
		public bool HasTriedOurServie { get; set; }
	}
	public class AddLeadNewRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
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
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }

		public object RunRequest(AddLeadNewRequest request)
		{
			var response = new AddLeadNewResponse();
			var AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
			var Lead = new LMS.Models.EntityModel.Lead();
			Lead.AgentId = AgentId;
			
			Lead.CreatedAt = DateTime.Now;
			Lead.Date = DateTime.Now.Date;
			Lead.CreatedBy = request.CreatedBy;
			//Lead.Name = request.Name;

			//CompanyInfo
			Lead.Address = request.CompanyDetails.Address;
			Lead.CompanyId = request.CompanyId;
			Lead.Email = request.CompanyDetails.Email;
			Lead.ModeOfCommunication = request.CompanyDetails.MOCId;
			Lead.BusinessOperationTime = request.CompanyDetails.BOT;
			Lead.AlternateNumber = request.CompanyDetails.CompanyAlternateContact;
			Lead.Contact = request.CompanyDetails.Contact;
			Lead.Website = request.CompanyDetails.Website;
			Lead.Area = request.CompanyDetails.Area;
			Lead.City = request.CompanyDetails.City;
			Lead.NoEmployee = request.CompanyDetails.NOE;
			Lead.BusinessIndustry = request.CompanyDetails.BusinessIndustry;
			Lead.NTN = request.CompanyDetails.NTN;
			Lead.NoLinks = request.CompanyDetails.NoLinks;

			Lead.Domain = request.Domain;
			//POCs
			Lead.ContactPersonName = request.ContactPersonName;
			Lead.ContactPersonNumber = request.ContactPersonNumber;
			Lead.ContactPersonEmail = request.ContactPersonEmail;
			Lead.ContactPersonTitle = request.ContactPersonTitle;
			Lead.ContactPersonDepartment = request.ContactPersonDepartment;

			if (request.Domain == (int)LMS.Models.Enums.Domain.Connectivity)
            {
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
		
			Lead.LeadStatus = (int)LeadStatus.Open;
			Lead.PmdStatus = (int)PmdStatus.None;
			Lead.QuotationStatus = (int)QuotationStatus.None;

			Lead.Date = DateTime.Now.Date;
			//Lead.IsApproved =
			var result = _dbContext.Lead.Add(Lead);
			if (request.Domain == (int)LMS.Models.Enums.Domain.SolutionSet)
			{
				var SolutionSet = new LMS.Models.EntityModel.SolutionDetails();
				SolutionSet.LeadId = result.Id;//request.SolutionDetails.LeadId;
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
				SolutionSet.CreatedBy = request.CreatedBy;
				var SolutionResult = _dbContext.SolutionDetails.Add(SolutionSet);
				_dbContext.SaveChanges();
			}

			////Mark Agent Present;
			//var Attendance = new LMS.Models.EntityModel.AgentAttendance();
			//var today = DateTime.Now.Date;
			//var TodaysAgentAttendance = _dbContext.AgentAttendance.Where(x => x.Date == today && x.AgentId == AgentId);
   //         if (TodaysAgentAttendance.Count() <= 1)
   //         {
			//	Attendance.AgentId = AgentId;
			//	Attendance.CreatedAt = DateTime.Now;
			//	Attendance.Date = DateTime.Now.Date;
			//	Attendance.IsPresent = true;
			//	var Attendanceresult = _dbContext.AgentAttendance.Add(Attendance);
			//	_dbContext.SaveChanges();
			//}
			_dbContext.SaveChanges();
			return response;
		}
	}
}