﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SupermarketMAPEntities : DbContext
    {
        public SupermarketMAPEntities()
            : base("name=SupermarketMAPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Product_Category> Product_Category { get; set; }
        public virtual DbSet<Product_In_Stock> Product_In_Stock { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Receipt_Details> Receipt_Details { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<GetCategoryValue_Result> GetCategoryValue()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCategoryValue_Result>("GetCategoryValue");
        }
    
        public virtual ObjectResult<GetProductsWithProviderAndCategoryName_Result> GetProductsWithProviderAndCategoryName()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetProductsWithProviderAndCategoryName_Result>("GetProductsWithProviderAndCategoryName");
        }
    
        public virtual ObjectResult<GetReceiptDetailsByReceiptId_Result> GetReceiptDetailsByReceiptId(Nullable<int> receipt_id)
        {
            var receipt_idParameter = receipt_id.HasValue ?
                new ObjectParameter("receipt_id", receipt_id) :
                new ObjectParameter("receipt_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptDetailsByReceiptId_Result>("GetReceiptDetailsByReceiptId", receipt_idParameter);
        }
    
        public virtual ObjectResult<GetReceiptMonthlyStatistics_Result> GetReceiptMonthlyStatistics()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptMonthlyStatistics_Result>("GetReceiptMonthlyStatistics");
        }
    
        public virtual ObjectResult<GetReceiptsWithCashierNames_Result> GetReceiptsWithCashierNames()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptsWithCashierNames_Result>("GetReceiptsWithCashierNames");
        }
    
        public virtual ObjectResult<GetReceiptsWithCashierNamesAndTotalAmount_Result> GetReceiptsWithCashierNamesAndTotalAmount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptsWithCashierNamesAndTotalAmount_Result>("GetReceiptsWithCashierNamesAndTotalAmount");
        }
    
        public virtual ObjectResult<GetRemainingStock_Result> GetRemainingStock()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRemainingStock_Result>("GetRemainingStock");
        }
    
        public virtual ObjectResult<GetStockDetails_Result> GetStockDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStockDetails_Result>("GetStockDetails");
        }
    
        public virtual ObjectResult<GetUsers_Result> GetUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUsers_Result>("GetUsers");
        }
    
        public virtual ObjectResult<Nullable<int>> InsertReceiptAndGetId(Nullable<int> idCashier, Nullable<System.DateTime> releaseDate, Nullable<double> receivedAmount)
        {
            var idCashierParameter = idCashier.HasValue ?
                new ObjectParameter("IdCashier", idCashier) :
                new ObjectParameter("IdCashier", typeof(int));
    
            var releaseDateParameter = releaseDate.HasValue ?
                new ObjectParameter("ReleaseDate", releaseDate) :
                new ObjectParameter("ReleaseDate", typeof(System.DateTime));
    
            var receivedAmountParameter = receivedAmount.HasValue ?
                new ObjectParameter("ReceivedAmount", receivedAmount) :
                new ObjectParameter("ReceivedAmount", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("InsertReceiptAndGetId", idCashierParameter, releaseDateParameter, receivedAmountParameter);
        }
    
        public virtual int InsertUser(string name, string password, Nullable<int> idRole)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var idRoleParameter = idRole.HasValue ?
                new ObjectParameter("idRole", idRole) :
                new ObjectParameter("idRole", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertUser", nameParameter, passwordParameter, idRoleParameter);
        }
    
        public virtual int UpdateRole(Nullable<int> roleId, string name)
        {
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("roleId", roleId) :
                new ObjectParameter("roleId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateRole", roleIdParameter, nameParameter);
        }
    
        public virtual int UpdateUser(Nullable<int> userId, string name, string password, Nullable<int> idRole)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var idRoleParameter = idRole.HasValue ?
                new ObjectParameter("idRole", idRole) :
                new ObjectParameter("idRole", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateUser", userIdParameter, nameParameter, passwordParameter, idRoleParameter);
        }
    
        public virtual ObjectResult<GetReceiptDetailsWithProductNames_Result> GetReceiptDetailsWithProductNames()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptDetailsWithProductNames_Result>("GetReceiptDetailsWithProductNames");
        }
    
        public virtual int InsertReceiptDetail(Nullable<int> idReceipt, Nullable<int> idStock, Nullable<int> quantity, Nullable<double> pricePerItem)
        {
            var idReceiptParameter = idReceipt.HasValue ?
                new ObjectParameter("idReceipt", idReceipt) :
                new ObjectParameter("idReceipt", typeof(int));
    
            var idStockParameter = idStock.HasValue ?
                new ObjectParameter("idStock", idStock) :
                new ObjectParameter("idStock", typeof(int));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("quantity", quantity) :
                new ObjectParameter("quantity", typeof(int));
    
            var pricePerItemParameter = pricePerItem.HasValue ?
                new ObjectParameter("pricePerItem", pricePerItem) :
                new ObjectParameter("pricePerItem", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertReceiptDetail", idReceiptParameter, idStockParameter, quantityParameter, pricePerItemParameter);
        }
    
        public virtual ObjectResult<GetReceiptWithUsername_Result> GetReceiptWithUsername(Nullable<int> receiptId)
        {
            var receiptIdParameter = receiptId.HasValue ?
                new ObjectParameter("receiptId", receiptId) :
                new ObjectParameter("receiptId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetReceiptWithUsername_Result>("GetReceiptWithUsername", receiptIdParameter);
        }
    }
}
