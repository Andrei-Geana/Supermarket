using Checkers_Game.Command;
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
using System.Windows;
using System.Windows.Input;
using System.Runtime;

namespace SupermarketApp.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private UserBLL _userBLL;
        private RoleBLL _roleBLL;
        private ObservableCollection<GetUsers_Result> _users;
        private ObservableCollection<Role> _roles;
        private GetUsers_Result _selectedUser;
        public UsersViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<RolesViewModel> createRolesMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToRolesMenu = new NavigationCommand(navigation, createRolesMenu);
            _userBLL = new UserBLL();
            _roleBLL = new RoleBLL();
            Roles = _roleBLL.GetRoles();
            ResetUser();

            AddUserCommand = new RelayCommand<object>(param => AddUser());
            ModifyUserCommand = new RelayCommand<object>(param => ModifyUser());
            DeleteUserCommand = new RelayCommand<object>(param => DeleteUser());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetUser());
        }


        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToRolesMenu { get; set; }
        public ObservableCollection<GetUsers_Result> UsersList 
        { 
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(UsersList));
            }
        }
        public ObservableCollection<Role> Roles { get => _roles; set => _roles = value; }
        public GetUsers_Result SelectedUser 
        { 
            get => _selectedUser;
            set 
            { 
                _selectedUser = value; 
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(SelectedUser.Role));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }

        public ICommand AddUserCommand { get; set; }
        public ICommand ModifyUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }


        private void AddUser()
        {
            try
            {
                if(SelectedUser.Name.Contains(' ') || SelectedUser.Password.Contains(' '))
                {
                    throw new Exception("Username and/or password must not contain blank characters");
                }
                if (string.IsNullOrEmpty(SelectedUser.Name) || string.IsNullOrEmpty(SelectedUser.Password) || string.IsNullOrEmpty(SelectedUser.Role))
                {
                    throw new Exception("All fields must be filled.");
                }
                User newUser = new User
                {
                    password = SelectedUser.Password,
                    name = SelectedUser.Name,
                    id_role = Roles.Where(role => role.name == SelectedUser.Role).FirstOrDefault().id
                };
                _userBLL.AddUser(newUser); 
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ResetUser();
            }
        }

        private void ModifyUser()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedUser.Name) || string.IsNullOrEmpty(SelectedUser.Password) || string.IsNullOrEmpty(SelectedUser.Role))
                {
                    throw new Exception("All fields must be filled.");
                }
                User newUser = new User
                {
                    id = SelectedUser.ID,
                    password = SelectedUser.Password,
                    name = SelectedUser.Name,
                    id_role = Roles.Where(role => role.name == SelectedUser.Role).FirstOrDefault().id
                };
                _userBLL.ModifyUser(newUser);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ResetUser();
            }
        }

        private void DeleteUser()
        {
            try
            {
                if(App.CurrentUser.id == SelectedUser.ID)
                {
                    throw new Exception("Unable to delete: Is app user.");
                }
                _userBLL.DeleteUserWithId(SelectedUser.ID);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ResetUser();
            }
        }
        private void ResetUser()
        {
            UsersList = _userBLL.GetUsersWithRoleName();
            SelectedUser = new GetUsers_Result();
        }
        public bool ModifyAndDeleteButtonsAreEnabled => SelectedUser != null && SelectedUser.ID != 0;
        public bool AddButtonIsEnabled => SelectedUser != null && SelectedUser.ID == 0 ;
    }
}
