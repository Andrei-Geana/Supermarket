using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class StocksViewModel : ViewModelBase
    {
        private ProductBLL _productBLL;
        private StockBLL _stockBLL;

        private Product_In_Stock _productIn_Stock;
        private GetProductsWithProviderAndCategoryName_Result _selectedProduct;
        private ObservableCollection<GetProductsWithProviderAndCategoryName_Result> _products;
        public StocksViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<StocksManagementViewModel> createStockManagementMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToStockManagementMenu = new NavigationCommand(navigation, createStockManagementMenu);
            AddButton = new RelayCommand<object>(param => AddStock());

            _productBLL = new ProductBLL();
            _stockBLL = new StockBLL();

            ProductList = _productBLL.GetProductsWithCategoryAndProviderName();
            ProductIn_Stock = new Product_In_Stock();

            ResetData();
        }

        private void AddStock()
        {
            try
            {
                if(ProductIn_Stock.buy_price <= 0 || ProductIn_Stock.initial_quantity <= 0)
                {
                    throw new Exception("Invalid data: Data must not be smaller than 0.");

                }

                if(ProductIn_Stock.arrival_date.Date > ProductIn_Stock.expiration_date.Date)
                {
                    throw new Exception("Invalid dates: Expiration date must be after arrival date.");
                }

                if(DateTime.Now.Date > ProductIn_Stock.expiration_date.Date)
                {
                    throw new Exception("Invalid dates: Expiration date must be after current date.");
                }

                if(ProductIn_Stock.expiration_date == DateTime.MinValue || ProductIn_Stock.arrival_date == DateTime.MinValue)
                {
                    throw new Exception("Invalid dates: Must be set.");
                }
                //if(DateTime.Now.Date > ProductIn_Stock.arrival_date.Date)
                //{
                //    throw new Exception("Invalid dates: Arrival date must be after current date.");
                //}

                if (string.IsNullOrEmpty(ProductIn_Stock.unit_of_measurement))
                {
                    throw new Exception("Invalid data: Unit field must be filled.");

                }

                if(ProductIn_Stock.unit_of_measurement.Any(item => !char.IsLetter(item)))
                {
                    throw new Exception("Invalid data: Unit field must have only letters.");
                }

                foreach (var caracter in ProductIn_Stock.unit_of_measurement)
                {
                    if (char.IsDigit(caracter))
                    {
                        throw new Exception("Invalid unit: Must not contain digits.");
                    }
                }
                Product_In_Stock newProduct = new Product_In_Stock
                {
                    id_product = SelectedProduct.id,
                    initial_quantity= ProductIn_Stock.initial_quantity,
                    remaining_quantity= ProductIn_Stock.initial_quantity,
                    unit_of_measurement = ProductIn_Stock.unit_of_measurement,
                    arrival_date = ProductIn_Stock.arrival_date,
                    expiration_date = ProductIn_Stock.expiration_date,
                    buy_price = ProductIn_Stock.buy_price,
                    sell_price = ProductIn_Stock.buy_price + double.Parse(ConfigurationManager.AppSettings["TVA"]),
                };
                _stockBLL.AddProductInStock(newProduct);
                ResetData();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetData()
        {
            ProductIn_Stock = new Product_In_Stock() 
            {
                arrival_date = DateTime.Now,
                expiration_date = DateTime.Now
            };
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToStockManagementMenu { get; set; }
        public ICommand AddButton { get; set; }
        public GetProductsWithProviderAndCategoryName_Result SelectedProduct 
        { 
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }
        public ObservableCollection<GetProductsWithProviderAndCategoryName_Result> ProductList 
        { 
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(ProductList));
            }        
        }

        public Product_In_Stock ProductIn_Stock 
        { 
            get => _productIn_Stock;
            set
            {
                _productIn_Stock = value;
                OnPropertyChanged(nameof(ProductIn_Stock));
                OnPropertyChanged(nameof(ProductIn_Stock.arrival_date));
                OnPropertyChanged(nameof(ProductIn_Stock.expiration_date));
            }
        }

        public bool AddButtonIsEnabled => SelectedProduct != null && SelectedProduct.id != 0;
    }
}