using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Company
{

    public class GetCompanyRequest
    {
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public object RunRequest(GetCompanyRequest req)
		{
			var response = new GetCompanyResponse();
			var Data = _dbContext.Company.Where(x=>x.Id == req.Id).FirstOrDefault();
			response.Id = Data.Id;
			response.Name = Data.Name;
			response.Description = Data.Description;
			response.Address = Data.Address;
			response.Contact = Data.Contact;
			response.Email = Data.Email;
			response.IsBranch = Data.IsBranch;
			response.IsParent = Data.IsParent;
			response.NoBracnhOffices = Data.NumberOfBranchOffices;
			response.Website = Data.Website;
			response.NTN = Data.NTN;
			response.CurrentItPlatform = Data.CurrentItPlatform;
			response.NoLinks = Data.NoLinks;
			response.Latitude = Data.Latitude;
			response.Longitude = Data.Longitude;

			response.CompanyAlternateContact = Data.AlternateNumber;
            if (Data.ParentCompanyId != null)
            {
				response.ParentCompanyId = Data.ParentCompanyId;
				response.ParentCompanyName = Data.ParentCompany.Name;
			}
            if (Data.ModeOfCommunication.HasValue)
            {
				response.MOCEnum = ((ModeOfCommunication)Data.ModeOfCommunication.Value).ToString();
				response.MOCId = Data.ModeOfCommunication;
			}
			if (Data.BusinessOperationTime.HasValue)
			{
				response.BOTEnum = ((BusinessOperationTime)Data.BusinessOperationTime.Value).ToString();
				response.BOT = Data.BusinessOperationTime;
			}
			if (Data.Area.HasValue)
			{
				response.Area = Data.Area;
				response.AreaEnum = ((Area)Data.Area.Value).ToString();
			}
			if (Data.City.HasValue)
			{
				response.City = Data.City;
				response.CityEnum = ((City)Data.City.Value).ToString();
			}
			if (Data.NoEmployee.HasValue)
			{
				response.NOE = Data.NoEmployee;
				response.NOEEnum = ((NumberOfEmployeed)Data.NoEmployee.Value).ToString();
			}
			if (Data.BusinessIndustry.HasValue)
			{
				response.BusinessIndustry = Data.BusinessIndustry;
				response.BusinessIndustryEnum = ((BusinessSegmentation)Data.BusinessIndustry.Value).ToString();
			}
			if (Data.CUDS.HasValue)
			{
				response.CUDS = Data.CUDS;
				response.CUDSEnum = ((DataServices)Data.CUDS.Value).ToString();
			}
			if (Data.CUDSService.HasValue)
			{
				response.CUDSService = Data.CUDS;
                if (Data.CUDS == (int)DataServices.WiWax)
                {
					response.CUDSServiceEnum = ((WiWax)Data.CUDSService.Value).ToString();
				}
				if (Data.CUDS == (int)DataServices.Fiber)
				{
					response.CUDSServiceEnum = ((Fiber)Data.CUDSService.Value).ToString();
				}
				if (Data.CUDS == (int)DataServices.VSAT)
				{
					response.CUDSServiceEnum = ((VSAT)Data.CUDSService.Value).ToString();
				}
				if (Data.CUDS == (int)DataServices.DSL)
				{
					response.CUDSServiceEnum = ((DSL)Data.CUDSService.Value).ToString();
				}
				if (Data.CUDS == (int)DataServices.Other)
				{
					response.CUDSServiceEnum = Data.CUDSOtherService;//((DSL)Data.CUDSService.Value).ToString();
				}
			}
			response.CreatedAt = Data.CreatedAt;
			response.CreatedBy = Data.CreatedBy;
			response.UpdatedAt = Data.UpdatedAt;
			response.UpdatedBy = Data.UpdatedBy;
			
			return response;
		
	}
}
    public class GetCompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
		public string ParentCompanyName { get; set; }
		public int? ParentCompanyId { get; set; }
		public bool? IsParent { get; set; }
		public bool? IsBranch { get; set; }
		public int? MOCId { get; set; }
		public string MOCEnum { get; set; }
		public int? BOT { get; set; }
		public string BOTEnum { get; set; }
		public string CompanyAlternateContact { get; set; }
		public string Website { get; set; }
		public int? Area { get; set; }
		public string AreaEnum { get; set; }
		public int? City { get; set; }
		public string CityEnum { get; set; }
		public int? NOE { get; set; } //Number of Employees may be change it to string
		public string NOEEnum { get; set; }
		public int? NoBracnhOffices { get; set; }  // need to add this field in database..
		public int? BusinessIndustry { get; set; }
		public string BusinessIndustryEnum { get; set; }
		public string NTN { get; set; }
		public string CurrentItPlatform { get; set; }
		public string FuturePlanOFExtention { get; set; } //not in db
		public int? CUDS { get; set; }
		public string CUDSEnum { get; set; }
		public int? CUDSService { get; set; }
		public string CUDSServiceEnum { get; set; }
		public string CUDSOtherService { get; set; }
		public int? NoLinks { get; set; }
		public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

	}
}