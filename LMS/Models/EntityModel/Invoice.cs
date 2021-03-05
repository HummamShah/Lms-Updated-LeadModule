//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.ChargeDetails = new HashSet<ChargeDetails>();
            this.InvoiceDetails = new HashSet<InvoiceDetails>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public bool IsPosted { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> AccountId { get; set; }
        public string InvoiceCode { get; set; }
        public string AttendantName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string NTN { get; set; }
        public string CompanyEmail { get; set; }
        public string LinkName { get; set; }
        public string PackageId { get; set; }
        public Nullable<decimal> GorssAmount { get; set; }
        public Nullable<int> AmountType { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TaxWitholdingAmount { get; set; }
        public Nullable<int> AgentId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<decimal> MonthlyCharges { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Agent Agent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeDetails> ChargeDetails { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}