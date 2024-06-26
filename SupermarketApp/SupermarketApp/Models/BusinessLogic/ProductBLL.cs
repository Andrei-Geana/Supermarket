﻿using System;
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
            var products = entities.Products.Where(item => item.deleted == false).ToList();
            _products = new ObservableCollection<Product>();
            foreach (var product in products)
            {
                _products.Add(product);
            }
        }

        public ObservableCollection<Product> GetProducts()
        {
            ReinitializeList();
            return _products;
        }

        public ObservableCollection<GetProductsWithProviderAndCategoryName_Result> GetProductsWithCategoryAndProviderName()
        {
            ObservableCollection<GetProductsWithProviderAndCategoryName_Result> returnedProducts = new ObservableCollection<GetProductsWithProviderAndCategoryName_Result>();
            var products = entities.GetProductsWithProviderAndCategoryName().ToList();
            foreach (var user in products)
            {
                returnedProducts.Add(user);
            }
            return returnedProducts;
        }

        public void DeleteProductWithId(int ID)
        {
            try
            {
                //need to update with only logic deletion
                //entities.Products.Remove(entities.Products.Where(product => product.id == ID).FirstOrDefault());
                entities.Products.Where(product => product.id == ID).FirstOrDefault().deleted = true;
                entities.SaveChanges();
                _products.Remove(_products.Where(product => product.id == ID).FirstOrDefault());
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Product to be deleted was not found in database.");
            }
        }

        public void AddProduct(Product newProduct)
        {
            try
            {
                entities.Products.Add(newProduct);
                entities.SaveChanges();
                _products.Add(newProduct);
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Product was not added to database.");
            }
        }

        public void ModifyProduct(Product productToBeModified)
        {
            try
            {
                var existingProduct = entities.Products.FirstOrDefault(product => product.id == productToBeModified.id) ?? throw new Exception();
                existingProduct.name = productToBeModified.name;
                existingProduct.bar_code = productToBeModified.bar_code;
                existingProduct.id_category = productToBeModified.id_category;
                existingProduct.id_provider = productToBeModified.id_provider;
                entities.SaveChanges();

                var currentProduct = _products.FirstOrDefault(product => product.id == productToBeModified.id);
                currentProduct.name = productToBeModified.name;
                currentProduct.bar_code = productToBeModified.bar_code;
                currentProduct.id_category = productToBeModified.id_category;
                currentProduct.id_provider = productToBeModified.id_provider;

            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Product was not modified in database.");
            }
        }
    }
}
