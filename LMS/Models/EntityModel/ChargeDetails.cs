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
    
    public partial class ChargeDetails
    {
        public int Id { get; set; }
        public int ChargeId { get; set; }
        public int InvoiceId { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> ValueType { get; set; }
        public Nullable<int> Type { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Name { get; set; }
    
        public virtual Charge Charge { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}