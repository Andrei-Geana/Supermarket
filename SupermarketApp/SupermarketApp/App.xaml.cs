﻿using SupermarketApp.Stores;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new LoginViewModel(_navigation, CreateMainMenu);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private MainMenuViewModel CreateMainMenu()
        {
            return new MainMenuViewModel(_navigation);
        }
    }
}
