//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupermarketApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products_In_Stock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products_In_Stock()
        {
            this.Receipt_Details = new HashSet<Receipt_Details>();
        }
    
        public int id { get; set; }
        public int id_product { get; set; }
        public int initial_quantity { get; set; }
        public int remaining_quantity { get; set; }
        public string unit_of_measurement { get; set; }
        public System.DateTime arrival_date { get; set; }
        public System.DateTime expiration_date { get; set; }
        public double buy_price { get; set; }
        public double sell_price { get; set; }
        public bool deleted { get; set; }
    
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt_Details> Receipt_Details { get; set; }
    }
}
