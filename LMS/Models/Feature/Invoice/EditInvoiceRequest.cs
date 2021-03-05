using LMS.Models.EntityModel;
using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Invoice
{

	public class EditInvoiceRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public DateTime DueDate { get; set; }
		public decimal? MonthlyCharges { get; set; }
		public bool IsPosted { get; set; }
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
		public DateTime? Date { get; set; }
		public string CompanyAddress { get; set; }
		public string CompanyEmail { get; set; }
		public string CompanyContact { get; set; }
		public string NTN { get; set; }
		public string LinkName { get; set; }
		public string PackageId { get; set; }
		public decimal? GrossAmount { get; set; }
		public decimal TaxWitholdingAmount { get; set; }
		public int? AgentId { get; set; }
		public List<InvoiceDetailsData> InvoiceDetails { get; set; }
		public List<InvoiceCharges> InvoiceTaxes { get; set; }
		public List<InvoiceCharges> DeletedCharges { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }

		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public object RunRequest(EditInvoiceRequest req)
		{
			var response = new EditInvoiceResponse();
		
			var Data = _dbContext.Invoice.Where(x => x.Id == req.Id).FirstOrDefault();
			Data.AccountId = req.AccountId;
			Data.AgentId = req.AgentId;
			Data.AttendantName = req.AttendantName;
			Data.CompanyAddress = req.CompanyAddress;
			Data.CompanyContact = req.CompanyContact;
			Data.CompanyEmail = req.CompanyEmail;
			Data.CompanyId = req.CompanyId;
			Data.Date = req.Date;
			Data.EndDate = req.EndDate;
			Data.GorssAmount = req.GrossAmount;
			Data.IsPosted = req.IsPosted;
			Data.LinkName = req.LinkName;
			Data.NTN = req.NTN;
			Data.PackageId = req.PackageId;
			Data.StartDate = req.StartDate;
			Data.Status = req.Status;
			
			Data.TaxWitholdingAmount = req.TaxWitholdingAmount;
			Data.TotalAmount = req.TotalAmount;
			Data.MonthlyCharges = req.MonthlyCharges;
			Data.DueDate = req.DueDate;
			Data.UpdatedAt = DateTime.Now;
			Data.UpdatedBy = req.UpdatedBy;

			foreach (var detail in req.InvoiceDetails)
			{
                if (detail.Id != 0)
                {
					var InvoiceDetail = _dbContext.InvoiceDetails.Where(x=>x.Id == detail.Id).FirstOrDefault();
					InvoiceDetail.Amount = detail.Amount;
					InvoiceDetail.Description = detail.Description;
					_dbContext.SaveChanges();
                }
                if (detail.Id == 0)
                {
					var InvoiceDetail = new InvoiceDetails();
					InvoiceDetail.InvoiceId = req.Id;
					InvoiceDetail.Amount = detail.Amount;
					InvoiceDetail.Description = detail.Description;
					_dbContext.InvoiceDetails.Add(InvoiceDetail);
					_dbContext.SaveChanges();
				}
				
				
			}


			var Taxes = req.InvoiceTaxes;

			foreach (var tax in Taxes)
			{
                if (tax.Id != 0)
                {
					var TaxData = _dbContext.ChargeDetails.Where(x=>x.Id == tax.Id).FirstOrDefault();
					TaxData.Name = tax.Name;
					TaxData.Amount = tax.Amount;
					TaxData.ChargeId = tax.ChargeId;
					TaxData.Value = tax.Value;
					_dbContext.SaveChanges();
				}
				if (tax.Id == 0)
				{
					var TaxData = new ChargeDetails();
					TaxData.Type = (int)ChargeType.Tax;
					TaxData.InvoiceId = req.Id;
					TaxData.Name = tax.Name;
					TaxData.Amount = tax.Amount;
					TaxData.ChargeId = tax.ChargeId;
					TaxData.Value = tax.Value;
					_dbContext.ChargeDetails.Add(TaxData);
					_dbContext.SaveChanges();
				}


			}
            if (req.DeletedCharges.Count > 0)
            {
                foreach (var row in req.DeletedCharges)
                {
					if (row.Id != 0)
					{
						var ChargeDetails = _dbContext.ChargeDetails.Where(x => x.Id == row.Id).FirstOrDefault();
						_dbContext.Entry(ChargeDetails).State = System.Data.Entity.EntityState.Deleted;
						_dbContext.SaveChanges();
					}
				}
            }
			return response;
		}
	}
	public class EditInvoiceResponse
	{
		public bool Success { get; set; }
		public List<string> ValidationErrors { get; set; }
	}
	
}