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
    
    public partial class GetStockDetails_Result
    {
        public int id { get; set; }
        public string product_name { get; set; }
        public string category_name { get; set; }
        public string provider_name { get; set; }
        public int initial_quantity { get; set; }
        public int remaining_quantity { get; set; }
        public string unit_of_measurement { get; set; }
        public System.DateTime arrival_date { get; set; }
        public System.DateTime expiration_date { get; set; }
        public double buy_price { get; set; }
        public double sell_price { get; set; }
    }
}