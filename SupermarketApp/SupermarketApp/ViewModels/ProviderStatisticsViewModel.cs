using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace SupermarketApp.ViewModels
{
    public class ProviderStatisticsViewModel : ViewModelBase
    {
        private ProductBLL _productBLL;
        private ProductCategoryBLL _categoryBLL;
        private ProviderBLL _providerBLL;

        private ObservableCollection<GetProductsWithProviderAndCategoryName_Result> _products;
        private ObservableCollection<Product_Category> _categories;
        private ObservableCollection<Provider> _providers;


        private string _selectedCategory = "";
        private string _selectedProvider = "";
        public ProviderStatisticsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<StatisticsViewModel> createReceiptStatistics)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToEarningsStatistics = new NavigationCommand(navigation, createReceiptStatistics);
            
            _categoryBLL = new ProductCategoryBLL();
            _providerBLL = new ProviderBLL();
            _productBLL = new ProductBLL();

            Categories = _categoryBLL.GetCategories();
            Providers = _providerBLL.GetProviders();

            Categories.Insert(0, new Product_Category() { name = "" });
            Providers.Insert(0, new Provider() { name = "" });

            ResetSelection();
        }

        private void ResetSelection()
        {
            Products = _productBLL.GetProductsWithCategoryAndProviderName();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToEarningsStatistics { get; set; }
        public ObservableCollection<Product_Category> Categories { get => _categories; set => _categories = value; }
        public ObservableCollection<Provider> Providers { get => _providers; set => _providers = value; }
        public ObservableCollection<GetProductsWithProviderAndCategoryName_Result> Products 
        {
            get 
            {
                ObservableCollection<GetProductsWithProviderAndCategoryName_Result> finalResult = new ObservableCollection<GetProductsWithProviderAndCategoryName_Result>();
                foreach (var item in _products)
                {
                    if (!string.IsNullOrEmpty(Provider) && item.ProviderName != Provider)
                        continue;
                    if (!string.IsNullOrEmpty(Category) && item.CategoryName != Category)
                        continue;
                    finalResult.Add(item);
                }
                return finalResult;
            } 
            set => _products = value; 
        }

        public string Category 
        { 
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(Products));
            }
            }
        public string Provider
        { 
            get => _selectedProvider;
            set
            { 
                _selectedProvider = value;
                OnPropertyChanged(nameof(Provider));
                OnPropertyChanged(nameof(Products));
            }
        }
    }
}
