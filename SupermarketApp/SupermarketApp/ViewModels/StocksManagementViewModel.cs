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

namespace SupermarketApp.ViewModels
{
    public class StocksManagementViewModel : ViewModelBase
    {
        public StocksManagementViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<StocksViewModel> createAddStockMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToAddStockMenu = new NavigationCommand(navigation, createAddStockMenu);


            ModifyStockMenu = new RelayCommand<object>(param => ModifyStock());
            DeleteStockMenu = new RelayCommand<object>(param => DeleteStock());

        }

        private void DeleteStock()
        {
            throw new NotImplementedException();
        }

        private void ModifyStock()
        {
            throw new NotImplementedException();
        }

        private void ResetData()
        {

        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToAddStockMenu { get; set; }
        public ICommand ModifyStockMenu { get; set; }
        public ICommand DeleteStockMenu { get; set; }
    }
}
