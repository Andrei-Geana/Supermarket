using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class ProductCategoryBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        public ObservableCollection<Product_Category> GetCategories()
        {
            var categories = entities.Product_Category.ToList();
            ObservableCollection<Product_Category> returnedCategories = new ObservableCollection<Product_Category>();
            foreach (var category in categories) { returnedCategories.Add(category); }
            return returnedCategories;
        }
    }
}
