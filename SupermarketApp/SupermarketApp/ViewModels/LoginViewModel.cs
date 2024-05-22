using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(Navigation navigation, Func<MainMenuViewModel> createMenuViewModel, Func<CashierViewModel> createCashierViewModel) 
        {
            NavigateToAdminMenu = new NavigationCommand(navigation, createMenuViewModel);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierViewModel);
            RegisterCommand = new RelayCommand<object>(param => Register());
            LoginCommand = new RelayCommand<object>(param => Login());
        }

        public ICommand LoginCommand { get; set; }
        public ICommand NavigateToAdminMenu { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string Username 
        { 
            get => App._username;
            set
            {
                App._username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(ButtonIsEnabled));
            }
        }
        public string Password 
        { 
            get => App._password;
            set
            {
                App._password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(ButtonIsEnabled));
            }
        }

        public bool ButtonIsEnabled => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        private void Register()
        {
            //checks if it can register
            if(false)
            {
                //register
                

                //automatically login
                Login();
            }
            else
            {
                MessageBox.Show("Unable to create account. Please enter other credentials.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Login()
        {
            //checks if login is successful
            if (true)
            {
                //checks if user is admin
                if (true)
                    NavigateToAdminMenu.Execute(null);
                else
                    NavigateToCashierMenu.Execute(null);
            }
            else
            {
                MessageBox.Show("Invalid credentials.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
