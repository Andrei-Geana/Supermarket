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

        public MainMenuViewModel(Navigation navigation, Func<LoginViewModel> createLoginMenu, Func<CashierViewModel> createCashierMenu
            , Func<UsersViewModel> createUsersMenu, Func<CategoriesViewModel> createCategoriesMenu, Func<RolesViewModel> createRolesMenu)
        {
            this.navigation = navigation;
            NavigateBackToLoginCommand = new NavigationCommand(navigation, createLoginMenu);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierMenu);
            NavigateToUsersMenu = new NavigationCommand(navigation, createUsersMenu);
            NavigateToCategoriesMenu = new NavigationCommand(navigation, createCategoriesMenu);
            NavigateToRolesMenu = new NavigationCommand(navigation, createRolesMenu);
        }

        public ICommand NavigateBackToLoginCommand { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
        public ICommand NavigateToUsersMenu { get; set; }
        public ICommand NavigateToCategoriesMenu { get; set; }
        public ICommand NavigateToRolesMenu { get; set; }
    }
}
