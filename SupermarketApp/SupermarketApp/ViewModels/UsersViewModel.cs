using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private UserBLL _userBLL;
        private ObservableCollection<GetUsers_Result> _users;
        public UsersViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            _userBLL = new UserBLL();
            UsersList = _userBLL.GetUsers();
        }
        public ICommand NavigateBackToMenu { get; set; }
        public ObservableCollection<GetUsers_Result> UsersList { get => _users; set => _users = value; }
    }
}
