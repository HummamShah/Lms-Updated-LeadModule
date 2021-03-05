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
    
    public partial class Lead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lead()
        {
            this.PmdDetails = new HashSet<PmdDetails>();
            this.SolutionDetails = new HashSet<SolutionDetails>();
            this.QuestionnareDetails = new HashSet<QuestionnareDetails>();
            this.PurchaseOrder = new HashSet<PurchaseOrder>();
        }
    
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Domain { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string AlternateNumber { get; set; }
        public string Website { get; set; }
        public Nullable<int> Area { get; set; }
        public Nullable<int> City { get; set; }
        public string Description { get; set; }
        public Nullable<int> ModeOfCommunication { get; set; }
        public Nullable<int> BusinessOperationTime { get; set; }
        public Nullable<int> NoEmployee { get; set; }
        public Nullable<int> BusinessIndustry { get; set; }
        public Nullable<int> NoLinks { get; set; }
        public string NTN { get; set; }
        public Nullable<int> NumberOfBranchOffices { get; set; }
        public Nullable<System.DateTime> EstimatedClosingDate { get; set; }
        public Nullable<bool> IsEsisting { get; set; }
        public Nullable<bool> HasTriedOurServie { get; set; }
        public string Comments { get; set; }
        public Nullable<int> AssignedPmdId { get; set; }
        public Nullable<System.DateTime> PmdAssignedOn { get; set; }
        public Nullable<System.DateTime> PresaleAssignedOn { get; set; }
        public Nullable<System.DateTime> LeadAssignedOn { get; set; }
        public Nullable<int> AssignedPreSaleId { get; set; }
        public Nullable<int> AssignedToId { get; set; }
        public string LeadRemarks { get; set; }
        public Nullable<int> LeadStatus { get; set; }
        public Nullable<int> PmdStatus { get; set; }
        public string PmdRemarks { get; set; }
        public Nullable<int> QuotationStatus { get; set; }
        public string QuotationRemarks { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string AdminRemarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<decimal> Budget { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonNumber { get; set; }
        public int AgentId { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonTitle { get; set; }
        public string ContactPersonDepartment { get; set; }
        public string RequiredMedium { get; set; }
        public string CurrentlyUsedMedium { get; set; }
        public string Bandwidth { get; set; }
        public Nullable<int> ConnectivityType { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> Quotation { get; set; }
        public decimal QuotationMRC { get; set; }
        public decimal QuotationOTC { get; set; }
    
        public virtual Agent AssignedTo { get; set; }
        public virtual Agent AssignedPMD { get; set; }
        public virtual Agent AssignedPreSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PmdDetails> PmdDetails { get; set; }
        public virtual Company Company { get; set; }
        public virtual Agent Agent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolutionDetails> SolutionDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionnareDetails> QuestionnareDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
