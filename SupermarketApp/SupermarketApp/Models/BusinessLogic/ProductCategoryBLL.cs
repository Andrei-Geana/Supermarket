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

        public void AddCategory(Product_Category newCategory)
        {
            try
            {
                entities.Product_Category.Add(newCategory);
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Category was not added to database.");
            }
        }

        public void ModifyCategory(Product_Category newCategory)
        {
            try
            {
                var existingCategory = entities.Product_Category.FirstOrDefault(category => category.id == newCategory.id) ?? throw new Exception("Category not found in database");
                existingCategory.name = newCategory.name;
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Category was not modified in database.");
            }
        }

        internal void DeleteCategoryWithId(int id)
        {
            try
            {
                //need to update with only logic deletion
                entities.Product_Category.Remove(entities.Product_Category.Where(category => category.id == id).FirstOrDefault());
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Category to be deleted was not found in database.");
            }
        }
    }
}
