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
    
    public partial class GetReceiptDetailsWithProductNames_Result
    {
        public int id { get; set; }
        public int id_receipt { get; set; }
        public int id_stock { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public double price_per_item { get; set; }
    }
}
