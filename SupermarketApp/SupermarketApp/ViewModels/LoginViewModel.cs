using Checkers_Game.Command;
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
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(Navigation navigation, Func<MainMenuViewModel> createMenuViewModel) 
        {
            LoginCommand = new NavigationCommand(navigation, createMenuViewModel);
            ResetCommand = new RelayCommand<object>(param => ResetFields());
        }

        private void ResetFields()
        {
            Username = "";
            Password = "";
        }

        public ICommand LoginCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public string Username 
        { 
            get => App._username;
            set
            {
                App._username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password 
        { 
            get => App._password;
            set
            {
                App._password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
