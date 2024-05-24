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
        private ObservableCollection<Product_Category> product_Categories;

        public ProductCategoryBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var categories = entities.Product_Category.ToList();
            product_Categories = new ObservableCollection<Product_Category>();
            foreach (var category in categories)
            {
                Product_Category newCategory = new Product_Category
                {
                    id = category.id,
                    name = category.name
                };
                product_Categories.Add(newCategory);
            }
        }
        public ObservableCollection<Product_Category> GetCategories()
        {
            ReinitializeList();
            return product_Categories;
        }

        public void AddCategory(Product_Category newCategory)
        {
            try
            {
                entities.Product_Category.Add(newCategory);
                entities.SaveChanges();
                product_Categories.Add(newCategory);
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
                var currentCategory = product_Categories.Where(category => category.id == newCategory.id).FirstOrDefault();
                currentCategory.name = newCategory.name;

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
                product_Categories.Remove(product_Categories.Where(category => category.id == id).FirstOrDefault());
            }
            catch
            {
                throw new Exception("Category to be deleted was not found in database.");
            }
        }
    }
}
