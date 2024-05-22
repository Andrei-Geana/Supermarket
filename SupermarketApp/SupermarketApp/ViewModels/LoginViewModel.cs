using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
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
        private UserBLL _userBLL;
        private RoleBLL _roleBLL;
        public LoginViewModel(Navigation navigation, Func<MainMenuViewModel> createMenuViewModel, Func<CashierViewModel> createCashierViewModel) 
        {
            NavigateToAdminMenu = new NavigationCommand(navigation, createMenuViewModel);
            NavigateToCashierMenu = new NavigationCommand(navigation, createCashierViewModel);
            LoginCommand = new RelayCommand<object>(param => Login());
            _userBLL = new UserBLL();
            _roleBLL = new RoleBLL();
        }

        public ICommand LoginCommand { get; set; }
        public ICommand NavigateToAdminMenu { get; set; }
        public ICommand NavigateToCashierMenu { get; set; }
        public string Username 
        { 
            get => App.CurrentUser.name;
            set
            {
                App.CurrentUser.name = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(ButtonIsEnabled));
            }
        }
        public string Password 
        { 
            get => App.CurrentUser.password;
            set
            {
                App.CurrentUser.password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(ButtonIsEnabled));
            }
        }

        public bool ButtonIsEnabled => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public void Login()
        {
            //checks if login is successful
            User user = _userBLL.Login(App.CurrentUser);
            if (user != null) 
            {
                App.CurrentUser = user;
                //checks if user is admin
                if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
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
