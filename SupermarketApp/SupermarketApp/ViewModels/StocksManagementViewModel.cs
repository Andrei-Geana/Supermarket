using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Models;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace SupermarketApp.ViewModels
{
    public class StocksManagementViewModel : ViewModelBase
    {
        private StockBLL _stockBLL;

        private ObservableCollection<GetStockDetails_Result> _stocks;
        private GetStockDetails_Result _selectedStock;
        public StocksManagementViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<StocksViewModel> createAddStockMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToAddStockMenu = new NavigationCommand(navigation, createAddStockMenu);
            _stockBLL = new StockBLL();


            ResetData();
            ModifyStockMenu = new RelayCommand<object>(param => ModifyStock());
            DeleteStockMenu = new RelayCommand<object>(param => DeleteStock());

        }
        public ObservableCollection<GetStockDetails_Result> Stocks 
        { 
            get => _stocks;
            set 
            { 
                _stocks = value; 
                OnPropertyChanged(nameof(Stocks));
            } 
        }
        public GetStockDetails_Result SelectedStock 
        { 
            get => _selectedStock;
            set
            {
                _selectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
            }
        }

        private void DeleteStock()
        {
            try
            {
                _stockBLL.DeleteStockWithID(SelectedStock.id);
                ResetData();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyStock()
        {
            try
            {
                if(SelectedStock.sell_price < SelectedStock.buy_price)
                {
                    throw new Exception("Invalid data: Sell price must be bigger than buy price.");
                }
                _stockBLL.ModifyStock(SelectedStock);
                ResetData();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetData()
        {
            Stocks = _stockBLL.GetStockDetails();
            SelectedStock = new GetStockDetails_Result();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToAddStockMenu { get; set; }
        public ICommand ModifyStockMenu { get; set; }
        public ICommand DeleteStockMenu { get; set; }
        public bool ModifyAndDeleteButtonsAreEnabled => SelectedStock != null && SelectedStock.id != 0;
    }
}
