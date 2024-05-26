using SupermarketApp.Models;
using SupermarketApp.Stores;
using SupermarketApp.ViewModels;
using SupermarketApp.Views;
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
            return new MainMenuViewModel(_navigation, CreateLoginMenu, CreateCashierMenu, CreateUsersMenu, CreateProductsMenu, CreateStocksManagementMenu, CreateReceiptMenu,
                CreateStatisticsMenu);
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
            return new UsersViewModel(_navigation, CreateMainMenu, CreateRolesMenu);
        }

        private CategoriesViewModel CreateCategoriesMenu()
        {
            return new CategoriesViewModel(_navigation, CreateMainMenu, CreateProductsMenu, CreateProvidersMenu);
        }

        private RolesViewModel CreateRolesMenu()
        {
            return new RolesViewModel(_navigation, CreateMainMenu, CreateUsersMenu);
        }
        private ProvidersViewModel CreateProvidersMenu()
        {
            return new ProvidersViewModel(_navigation, CreateMainMenu, CreateProductsMenu, CreateCategoriesMenu);
        }

        private ProductsViewModel CreateProductsMenu()
        {
            return new ProductsViewModel(_navigation, CreateMainMenu, CreateProvidersMenu, CreateCategoriesMenu);
        }

        private StocksViewModel CreateStocksMenu()
        {
            return new StocksViewModel(_navigation, CreateMainMenu, CreateStocksManagementMenu);
        }

        private StocksManagementViewModel CreateStocksManagementMenu() 
        {
            return new StocksManagementViewModel(_navigation, CreateMainMenu, CreateStocksMenu);
        }

        private ReceiptMenuViewModel CreateReceiptMenu()
        {
            return new ReceiptMenuViewModel(_navigation, CreateMainMenu);
        }

        private StatisticsViewModel CreateStatisticsMenu()
        {
            return new StatisticsViewModel(_navigation, CreateMainMenu, CreateProviderStatisticsMenu);
        }

        private ProviderStatisticsViewModel CreateProviderStatisticsMenu()
        {
            return new ProviderStatisticsViewModel(_navigation, CreateMainMenu, CreateStatisticsMenu);
        }
        private void ResetCurrentUser()
        {
            CurrentUser = new User();
        }
    }
}
