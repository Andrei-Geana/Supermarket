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
        private Navigation navigation;

        public MainMenuViewModel() { }

        public MainMenuViewModel(Navigation navigation, Func<LoginViewModel> createLoginMenu, Func<CashierViewModel> createCashierMenu)
        {
            this.navigation = navigation;
            NavigateBackToLoginCommand = new NavigationCommand(navigation, createLoginMenu);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierMenu);
        }

        public ICommand NavigateBackToLoginCommand { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
    }
}
