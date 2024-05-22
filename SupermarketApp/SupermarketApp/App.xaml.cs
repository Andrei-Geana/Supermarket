using SupermarketApp.Models;
using SupermarketApp.Stores;
using SupermarketApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SupermarketApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Navigation _navigation = new Navigation();

        //placeholder for current user
        public static User CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = CreateLoginMenu();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private MainMenuViewModel CreateMainMenu()
        {
            return new MainMenuViewModel(_navigation, CreateLoginMenu, CreateCashierMenu, CreateUsersMenu);
        }

        private LoginViewModel CreateLoginMenu()
        {
            ResetCurrentUser();
            return new LoginViewModel(_navigation, CreateMainMenu, CreateCashierMenu);
        }

        private CashierViewModel CreateCashierMenu()
        {
            return new CashierViewModel(_navigation, CreateMainMenu, CreateLoginMenu);
        }

        private UsersViewModel CreateUsersMenu()
        {
            return new UsersViewModel(_navigation, CreateMainMenu);
        }

        private void ResetCurrentUser()
        {
            CurrentUser = new User();

        }
    }
}
