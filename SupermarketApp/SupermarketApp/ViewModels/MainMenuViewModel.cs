using SupermarketApp.Commands;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel() { }

        public MainMenuViewModel(Navigation navigation, Func<LoginViewModel> createLoginMenu, Func<CashierViewModel> createCashierMenu
            , Func<UsersViewModel> createUsersMenu, Func<ProductsViewModel> createProductsMenu, Func<StocksManagementViewModel> createStocksManagementMenu, 
            Func<ReceiptMenuViewModel> createReceiptMenu)
        {
            NavigateBackToLoginCommand = new NavigationCommand(navigation, createLoginMenu);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierMenu);
            NavigateToUsersMenu = new NavigationCommand(navigation, createUsersMenu);
            NavigateToProductsMenu = new NavigationCommand(navigation, createProductsMenu);
            NavigateToStocksMenu = new NavigationCommand(navigation, createStocksManagementMenu);
            NavigateToReceiptMenu = new NavigationCommand(navigation, createReceiptMenu);
        }

        public ICommand NavigateBackToLoginCommand { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
        public ICommand NavigateToUsersMenu { get; set; }
        public ICommand NavigateToProductsMenu { get; set; }
        public ICommand NavigateToStocksMenu { get; set; }
        public ICommand NavigateToReceiptMenu { get; set; }
    }
}
