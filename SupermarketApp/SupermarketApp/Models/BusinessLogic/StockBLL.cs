using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class StockBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        private ObservableCollection<Product_In_Stock> _productsInStock;

        public StockBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var products = entities.Product_In_Stock.Where(item => item.deleted == false).ToList();
            _productsInStock = new ObservableCollection<Product_In_Stock>();
            foreach (var product in products)
            {
                _productsInStock.Add(product);
            }
        }

        public ObservableCollection<Product_In_Stock> GetProductsInStocks()
        {
            ReinitializeList();
            return _productsInStock;
        }

        public void AddProductInStock(Product_In_Stock newStock)
        {
            try
            {
                entities.Product_In_Stock.Add(newStock);
                entities.SaveChanges();
                _productsInStock.Add(newStock);
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Stock was not added to database.");
            }
        }

        public ObservableCollection<GetStockDetails_Result> GetStockDetails()
        {
            var data = entities.GetStockDetails().ToList();
            ObservableCollection< GetStockDetails_Result > result = new ObservableCollection< GetStockDetails_Result >();
            foreach( var item in data) result.Add(item);
            return result;
        }

        public void ModifyStock(GetStockDetails_Result stock)
        {
            try
            {
                var stockInDatabase = entities.Product_In_Stock.Where(stockInDb => stockInDb.id == stock.id).FirstOrDefault();
                stockInDatabase.sell_price = stock.sell_price;
                stockInDatabase.remaining_quantity = stock.remaining_quantity;
                entities.SaveChanges();
                var stockInList = _productsInStock.Where(stockInDb => stockInDb.id == stock.id).FirstOrDefault();
                stockInList.sell_price = stock.sell_price;
                stockInList.remaining_quantity = stock.remaining_quantity;
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Stock was not modified in database.");
            }
        }

        public void DeleteStockWithID(int id)
        {
            try
            {
                //need to update with only logic deletion
                //entities.Product_In_Stock.Remove(entities.Product_In_Stock.Where(stockInDb => stockInDb.id == id).FirstOrDefault());
                entities.Product_In_Stock.Where(stockInDb => stockInDb.id == id).FirstOrDefault().deleted=true;
                entities.SaveChanges();
                _productsInStock.Remove(_productsInStock.Where(stockInList => stockInList.id == id).FirstOrDefault());
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Stock to be deleted was not found in database.");
            }
        }

        public ObservableCollection<GetRemainingStock_Result> GetRemainingStock()
        {
            var data = entities.GetRemainingStock().ToList().OrderBy(stock => stock.arrival_date);
            ObservableCollection<GetRemainingStock_Result> result = new ObservableCollection<GetRemainingStock_Result>();
            foreach (var item in data) result.Add(item);
            return result;
        }
    }
}
