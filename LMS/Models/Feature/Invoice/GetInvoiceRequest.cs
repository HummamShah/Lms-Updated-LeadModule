using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Invoice
{
    
	public class GetInvoiceRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public object RunRequest(GetInvoiceRequest req)
		{
			var response = new GetInvoiceResponse();
			response.InvoiceTaxes = new List<InvoiceCharges>();
			response.InvoiceDetails = new List<InvoiceDetailsData>();
			var Data = _dbContext.Invoice.Where(x => x.Id == req.Id).FirstOrDefault();
			response.Id = Data.Id;
			response.AccountId = Data.AccountId;
			response.AccountName = Data.Account.Name;
			response.AgentId = Data.AgentId;
			response.AttendantName = Data.AttendantName;
			response.CompanyAddress = Data.CompanyAddress;
			response.CompanyContact = Data.CompanyContact;
			response.CompanyEmail = Data.CompanyEmail;
			response.CompanyId = Data.CompanyId;
			response.CompanyName = Data.Company.Name;
			response.City = ((City)Data.Company.City).ToString();
			response.EndDate = Data.EndDate;
			response.GrossAmount = Data.GorssAmount;
			response.IsPosted = Data.IsPosted;
			response.LinkName = Data.LinkName;
			response.NTN = Data.NTN;
			response.PackageId = Data.PackageId;
			response.StartDate = Data.StartDate;
			response.Status = Data.Status;
			response.StatusEnum = ((InvoiceStatus)Data.Status).ToString();
			response.TaxWitholdingAmount = Data.TaxWitholdingAmount;
			response.TotalAmount = Data.TotalAmount;
			response.MonthlyCharges = Data.MonthlyCharges;
			response.DueDate = Data.DueDate;
			response.Date = Data.Date;
			response.CreatedAt = Data.CreatedAt;
			response.CreatedBy = Data.CreatedBy;
			response.UpdatedAt = Data.UpdatedAt;
			response.UpdatedBy = Data.UpdatedBy;

            foreach (var detail in Data.InvoiceDetails)
            {
				var InvoiceDetail = new InvoiceDetailsData();
				InvoiceDetail.Id = detail.Id;
				InvoiceDetail.Amount = detail.Amount;
				InvoiceDetail.Description = detail.Description;
				//InvoiceDetail.
				response.InvoiceDetails.Add(InvoiceDetail);
            }


			var Taxes = Data.ChargeDetails.Where(x => x.Type == (int)ChargeType.Tax);

			foreach (var tax in Taxes)
            {
				var TaxData = new InvoiceCharges();
				TaxData.Id = tax.Id;
				TaxData.Name = tax.Name;
				TaxData.Type= ((ChargeType)tax.Type.Value).ToString();
				TaxData.Amount = tax.Amount;
				TaxData.Value = tax.Value;
				TaxData.ChargeId = tax.ChargeId;
				response.InvoiceTaxes.Add(TaxData);
			}
			return response;
		}
	}
	public class GetInvoiceResponse
	{
		public int Id { get; set; }
		public bool IsPosted { get; set; }
		public string City { get; set; }
		public int Status { get; set; }
		public string StatusEnum { get; set; }
		public int CompanyId { get; set; }
		public string CompanyName { get; set; }
		public int? AccountId { get; set; }
		public string AttendantName { get; set; }
		public string AccountName { get; set; }
		public decimal? TotalAmount { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public string CompanyAddress { get; set; }
		public string CompanyEmail { get; set; }
		public string CompanyContact { get; set; }
		public decimal? MonthlyCharges { get; set; }
		public string NTN { get; set; }
		public string LinkName { get; set; }
		public string PackageId { get; set; }
		public decimal? GrossAmount { get; set; }
		public decimal? TaxWitholdingAmount { get; set; }
		public int? AgentId { get; set; }
		public DateTime? DueDate { get; set; }
		public List<InvoiceDetailsData> InvoiceDetails { get; set; }
		public List<InvoiceCharges> InvoiceTaxes { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }

		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public DateTime? Date { get; set; }

	}
	public class InvoiceDetailsData
    {
		public int Id { get; set; }
		public decimal? Amount { get; set; }
		public decimal Discount { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
	public class InvoiceCharges
    {
		public int Id { get; set; }
		public int ChargeId { get; set; }
		public decimal? Amount { get; set; }
		public decimal? Value { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
	}
}