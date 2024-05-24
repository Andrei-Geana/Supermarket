using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class ProductBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        private ObservableCollection<Product> _products;

        public ProductBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var products = entities.Products.ToList();
            _products = new ObservableCollection<Product>();
            foreach (var product in products)
            {
                _products.Add(product);
            }
        }

        ObservableCollection<Product> GetProducts()
        {
            ReinitializeList();
            return _products;
        }
    }
}
