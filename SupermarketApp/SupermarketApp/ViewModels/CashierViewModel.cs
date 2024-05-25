using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class CashierViewModel : ViewModelBase
    {
        private RoleBLL _roleBLL;
        private StockBLL _stockBLL;
        private ProductBLL _productBLL;
        private ProductCategoryBLL _categoryBLL;
        private ProviderBLL _providerBLL;

        private ObservableCollection<Product_Category> _categories;
        private ObservableCollection<Provider> _providers;
        private ObservableCollection<GetRemainingStock_Result> _stocks;
        private GetRemainingStock_Result _selectedStock;

        private string _barcode = "";
        private string _name = "";
        private string _provider = "";
        private string _category = "";
        private DateTime _expirationDate;
        public CashierViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<LoginViewModel> createLoginMenu)
        {
            NavigateBackToLogin = new NavigationCommand(navigation, createLoginMenu);
            NavigateBackToMainMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateBack = new RelayCommand<object>(param => DecideWhereToGoBack());

            _stockBLL = new StockBLL();
            _stockBLL = new StockBLL();
            _roleBLL = new RoleBLL();
            _categoryBLL = new ProductCategoryBLL();
            _providerBLL = new ProviderBLL();


            Categories = _categoryBLL.GetCategories();
            Providers = _providerBLL.GetProviders();

            Categories.Insert(0, new Product_Category() { name = "" });
            Providers.Insert(0, new Provider() { name = "" });
            ResetSelection();


        }

        public ICommand NavigateBackToLogin { get; set; }
        public ICommand NavigateBackToMainMenu { get; set; }
        public ICommand NavigateBack { get; set; }

        public void ResetSelection()
        {
            Stocks = _stockBLL.GetRemainingStock();
            SelectedStock = new GetRemainingStock_Result();
        }














        private void DecideWhereToGoBack()
        {
            //if current user is admin
            if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
            {
                NavigateBackToMainMenu.Execute(null);
            }
            else
            {
                NavigateBackToLogin.Execute(null);
            }
        }

        public string TextButton
        {
            get
            {
                if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
                {
                    return "BACK";
                }
                else
                {
                    return "LOG OUT";
                }
            }
        }

        public ObservableCollection<GetRemainingStock_Result> Stocks 
        { 
            get
            {
                ObservableCollection<GetRemainingStock_Result> finalResult = new ObservableCollection<GetRemainingStock_Result>();
                foreach(var item in _stocks)
                {
                    if (!item.bar_code.StartsWith(Barcode))
                        continue;
                    if (!item.product_name.StartsWith(Name))
                        continue;
                    if (!string.IsNullOrEmpty(Provider) && item.provider_name != Provider)
                        continue;
                    if (!string.IsNullOrEmpty(Category) && item.category_name != Category)
                        continue;
                    if (item.expiration_date < ExpirationDate)
                        continue;
                    finalResult.Add(item);
                }
                return finalResult;
            }
            set => _stocks = value; 
        }
        public GetRemainingStock_Result SelectedStock { get => _selectedStock; set { _selectedStock = value; OnPropertyChanged(nameof(SelectedStock)); } }

        public string Barcode { get => _barcode; set { _barcode = value; OnPropertyChanged(nameof(Barcode)); OnPropertyChanged(nameof(Stocks)); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(Stocks)); } }
        public string Provider { get => _provider; set { _provider = value; OnPropertyChanged(nameof(Provider)); OnPropertyChanged(nameof(Stocks)); } }
        public string Category { get => _category; set { _category = value; OnPropertyChanged(nameof(Category)); OnPropertyChanged(nameof(Stocks)); } }
        public DateTime ExpirationDate { get => _expirationDate; set { _expirationDate = value; OnPropertyChanged(nameof(ExpirationDate)); OnPropertyChanged(nameof(Stocks)); } }

        public ObservableCollection<Product_Category> Categories { get => _categories; set => _categories = value; }
        public ObservableCollection<Provider> Providers { get => _providers; set => _providers = value; }
    }
}
