using Checkers_Game.Command;
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
using System.Windows;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class ProductsViewModel : ViewModelBase 
    {
        private ProductBLL _productBLL;
        private ProductCategoryBLL _productCategoryBLL;
        private ProviderBLL _providerBLL;

        private ObservableCollection<GetProductsWithProviderAndCategoryName_Result> _products;
        private ObservableCollection<Product_Category> _categories;
        private ObservableCollection<Provider> _providers;
        private GetProductsWithProviderAndCategoryName_Result _selectedProduct;
        public ProductsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProvidersViewModel> createProvidersMenu, Func<CategoriesViewModel> createCategoriesMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProviders= new NavigationCommand(navigation, createProvidersMenu);
            NavigateToCategories= new NavigationCommand(navigation, createCategoriesMenu);

            _productBLL = new ProductBLL();
            _productCategoryBLL = new ProductCategoryBLL();
            _providerBLL = new ProviderBLL();

            Categories = _productCategoryBLL.GetCategories();
            Providers = _providerBLL.GetProviders();

            ResetProduct();

            AddProductCommand = new RelayCommand<object>(param => AddProduct());
            ModifyProductCommand = new RelayCommand<object>(param => ModifyProduct());
            DeleteProductCommand = new RelayCommand<object>(param => DeleteProduct());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetProduct());
        }

        public ObservableCollection<Product_Category> Categories 
        { 
            get => _categories;
            set 
            { 
                _categories = value; 
                OnPropertyChanged(nameof(Categories)); 
            }
        }
        public ObservableCollection<Provider> Providers 
        { 
            get => _providers;
            set 
            { 
                _providers = value; 
                OnPropertyChanged(nameof(Providers)); 
            }
        }
        public ObservableCollection<GetProductsWithProviderAndCategoryName_Result> Products 
        { 
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public GetProductsWithProviderAndCategoryName_Result SelectedProduct 
        { 
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }


        private void ResetProduct()
        {
            Products = _productBLL.GetProductsWithCategoryAndProviderName();
            SelectedProduct = new GetProductsWithProviderAndCategoryName_Result();
        }

        private void DeleteProduct()
        {
            try
            {
                _productBLL.DeleteProductWithId(SelectedProduct.id);
                ResetProduct();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyProduct()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedProduct.name) || string.IsNullOrEmpty(SelectedProduct.bar_code)
                    || string.IsNullOrEmpty(SelectedProduct.CategoryName) || string.IsNullOrEmpty(SelectedProduct.ProviderName))
                {
                    throw new Exception("All fields must be filled.");
                }
                if (SelectedProduct.bar_code.Length != 8)
                {
                    throw new Exception("Invalid Barcode: Must be of length 8.");
                }
                foreach (var character in SelectedProduct.bar_code)
                {
                    if (!char.IsDigit(character))
                    {
                        throw new Exception("Invalid barcode: Must contain only digits.");
                    }
                }
                Product newProduct = new Product
                {
                    id = SelectedProduct.id,
                    name = SelectedProduct.name,
                    bar_code = SelectedProduct.bar_code,
                    id_category = Categories.Where(category => category.name.Equals(SelectedProduct.CategoryName)).First().id,
                    id_provider = Providers.Where(provider => provider.name.Equals(SelectedProduct.ProviderName)).First().id,
                };
                _productBLL.ModifyProduct(newProduct);
                ResetProduct();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProduct()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedProduct.name) || string.IsNullOrEmpty(SelectedProduct.bar_code) 
                    || string.IsNullOrEmpty(SelectedProduct.CategoryName) || string.IsNullOrEmpty(SelectedProduct.ProviderName))
                {
                    throw new Exception("All fields must be filled.");
                }
                if(SelectedProduct.bar_code.Length!=8)
                {
                    throw new Exception("Invalid Barcode: Must be of length 8.");
                }
                foreach(var caracter in SelectedProduct.bar_code)
                {
                    if(!char.IsDigit(caracter))
                    {
                        throw new Exception("Invalid barcode: Must contain only digits.");
                    }
                }
                Product newProduct = new Product
                {
                    name = SelectedProduct.name,
                    bar_code = SelectedProduct.bar_code,
                    id_category = Categories.Where(category => category.name.Equals(SelectedProduct.CategoryName)).First().id,
                    id_provider = Providers.Where(provider=> provider.name.Equals(SelectedProduct.ProviderName)).First().id,
                };
                _productBLL.AddProduct(newProduct);
                ResetProduct();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProviders { get; set; }
        public ICommand NavigateToCategories { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand ModifyProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }
        public bool ModifyAndDeleteButtonsAreEnabled => SelectedProduct != null && SelectedProduct.id != 0;
        public bool AddButtonIsEnabled => SelectedProduct != null && SelectedProduct.id == 0;

    }
}
