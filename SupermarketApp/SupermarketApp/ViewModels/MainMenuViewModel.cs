﻿using SupermarketApp.Commands;
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
            , Func<UsersViewModel> createUsersMenu, Func<RolesViewModel> createRolesMenu, 
            Func<ProductsViewModel> createProductsMenu)
        {
            NavigateBackToLoginCommand = new NavigationCommand(navigation, createLoginMenu);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierMenu);
            NavigateToUsersMenu = new NavigationCommand(navigation, createUsersMenu);
            NavigateToRolesMenu = new NavigationCommand(navigation, createRolesMenu);
            NavigateToProductsMenu = new NavigationCommand(navigation, createProductsMenu);
        }

        public ICommand NavigateBackToLoginCommand { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
        public ICommand NavigateToUsersMenu { get; set; }
        public ICommand NavigateToRolesMenu { get; set; }
        public ICommand NavigateToProductsMenu { get; set; }
    }
}
