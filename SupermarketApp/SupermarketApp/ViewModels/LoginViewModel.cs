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
        }

        public ICommand LoginCommand { get; set; }
    }
}
