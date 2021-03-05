using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{
   
	public class EditCompanyResponse
	{
		public bool IsVendorAdded { get; set; }
		public IEnumerable<string> ValidationErrors { get; set; }
	}
	public class EditCompanyRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Contact { get; set; }
		public int? ParentCompanyId { get; set; }
		public bool? IsBranch { get; set; }
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
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public object RunRequest(EditCompanyRequest request)
		{
			var response = new EditCompanyResponse();
			var Company = _dbContext.Company.FirstOrDefault(x=>x.Id == request.Id);
			Company.UpdatedAt = DateTime.Today;
			Company.UpdatedBy = request.UpdatedBy;
			Company.Description = request.Description;
			Company.Name = request.Name;
			Company.Address = request.Address;
			Company.Email = request.Email;
			Company.Contact = request.Contact;
			Company.Latitude = request.Latitude;
			Company.Longitude = request.Longitude;
			
			if (request.IsBranch == false && Company.IsBranch==true)
			{
				//Company.IsParent = true;
				Company.ParentCompanyId = null;
				
			}
            if (request.IsBranch == false)
            {
				Company.IsParent = true;
			}
			if (request.IsBranch == true)
			{
				Company.IsParent = false;
				Company.ParentCompanyId = request.ParentCompanyId;

			}
			Company.IsBranch = request.IsBranch;
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
			_dbContext.SaveChanges();
			return response;
		}
	}
}