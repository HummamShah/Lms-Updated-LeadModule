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
    
    public partial class Charge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Charge()
        {
            this.ChargeDetails = new HashSet<ChargeDetails>();
        }
    
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
        public int ValueType { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChargeDetails> ChargeDetails { get; set; }
    }
}
