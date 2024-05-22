using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class CashierViewModel : ViewModelBase
    {
        private RoleBLL _roleBLL;
        public CashierViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<LoginViewModel> createLoginMenu)
        {
            NavigateBackToLogin = new NavigationCommand(navigation, createLoginMenu);
            NavigateBackToMainMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateBack = new RelayCommand<object>(param => DecideWhereToGoBack());
            _roleBLL = new RoleBLL();
        }

        public ICommand NavigateBackToLogin { get; set; }
        public ICommand NavigateBackToMainMenu { get; set; }
        public ICommand NavigateBack { get; set; }

        private void DecideWhereToGoBack()
        {
            //if current user is admin
            if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
            {
                NavigateBackToMainMenu.Execute(null);
            }
            else
            {
                NavigateBackToLogin.Execute(null);
            }
        }
    }
}
