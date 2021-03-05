using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
  
	public class AddCompanyResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class AddCompanyRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		
		public string Name { get; set; }
		public string UserId { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public int? ParentCompanyId { get; set; }
		public bool IsBranch { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
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
		public string CurrentItPlatform { get; set; }
		public string FuturePlanOFExtention { get; set; } //not in db
		public int? CUDS { get; set; }
		public int? CUDSService { get; set; }
		public string CUDSOtherService { get; set; }
		public int NoLinks { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

		public object RunRequest(AddCompanyRequest request)
		{

			var response = new AddCompanyResponse();
			var AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
			var Company = new LMS.Models.EntityModel.Company();
			Company.AgentId = AgentId;
			Company.CreatedAt = DateTime.Today;
			Company.CreatedBy = request.CreatedBy;
			Company.Description = request.Description;
			Company.Name = request.Name;
			Company.Address = request.Address;
			Company.Email = request.Email;
			Company.Contact = request.Contact;
			Company.Latitude = request.Latitude;
			Company.Longitude = request.Longitude;
			Company.IsBranch = request.IsBranch;
            if (request.IsBranch == false)
            {
				Company.IsParent = true;
            }
            if (request.IsBranch == true)
            {
				Company.IsParent = false;
				Company.ParentCompanyId = request.ParentCompanyId;
			}
			Company.ModeOfCommunication = request.MOCId;
			Company.BusinessOperationTime = request.BOT;
			Company.AlternateNumber = request.CompanyAlternateContact;
			Company.Website = request.Website;
			Company.Area = request.Area;
			Company.City = request.City;
			Company.NoEmployee = request.NOE;
			Company.BusinessIndustry = request.BusinessIndustry;
			Company.NTN = request.NTN;
			Company.NumberOfBranchOffices = request.NoBracnhOffices;
			Company.CurrentItPlatform = request.CurrentItPlatform;
			Company.CUDS = request.CUDS;
			Company.CUDSOtherService = request.CUDSOtherService;
			Company.CUDSService = request.CUDSService;
			Company.NoLinks = request.NoLinks;
			var result = _dbContext.Company.Add(Company);
			_dbContext.SaveChanges();
			return response;
		}
	}
}