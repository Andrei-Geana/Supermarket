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
    public class StocksViewModel : ViewModelBase
    {
        private const double TVA = 2.5;

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

        }

        private void AddStock()
        {
            try
            {
                if(ProductIn_Stock.buy_price <= 0 || ProductIn_Stock.initial_quantity <= 0)
                {
                    throw new Exception("Invalid data: Data must not be smaller than 0.");

                }
                if(ProductIn_Stock.arrival_date > ProductIn_Stock.expiration_date)
                {
                    throw new Exception("Invalid dates: Expiration date must be after arrival date.");

                }
                if(DateTime.Now > ProductIn_Stock.expiration_date)
                {
                    throw new Exception("Invalid dates: Expiration date must be after current date.");
                }

                if (string.IsNullOrEmpty(ProductIn_Stock.unit_of_measurement))
                {
                    throw new Exception("Invalid data: Unit field must be filled.");

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
                    sell_price = ProductIn_Stock.sell_price + TVA,
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
            SelectedProduct = new GetProductsWithProviderAndCategoryName_Result();
            ProductIn_Stock = new Product_In_Stock();
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
            }
        }

        public bool AddButtonIsEnabled => SelectedProduct != null && SelectedProduct.id != 0;
    }
}