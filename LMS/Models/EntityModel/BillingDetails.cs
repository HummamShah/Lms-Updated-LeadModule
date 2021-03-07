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
    
    public partial class BillingDetails
    {
        public int Id { get; set; }
        public int BillingInformationId { get; set; }
        public string BillingName { get; set; }
        public Nullable<int> SalesAgentId { get; set; }
        public Nullable<bool> IsPaperBillRequired { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> InstallationDate { get; set; }
        public decimal OTC { get; set; }
        public decimal MRC { get; set; }
        public string Package { get; set; }
        public string Medium { get; set; }
        public string Description { get; set; }
        public Nullable<int> BillingStatus { get; set; }
        public bool IsDirectBilling { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual BillingInformation BillingInformation { get; set; }
    }
}
