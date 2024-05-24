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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Product_In_Stock = new HashSet<Product_In_Stock>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int id_category { get; set; }
        public int id_provider { get; set; }
        public string bar_code { get; set; }
        public bool deleted { get; set; }
    
        public virtual Product_Category Product_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_In_Stock> Product_In_Stock { get; set; }
        public virtual Provider Provider { get; set; }
    }
}