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
            var products = entities.Product_In_Stock.ToList();
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
                throw new Exception("Stock was not added to database.");
            }
        }
    }
}
