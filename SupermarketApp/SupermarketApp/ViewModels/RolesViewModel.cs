using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Models;
using SupermarketApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SupermarketApp.ViewModels
{
    public class RolesViewModel : ViewModelBase
    {
        private RoleBLL _roleBLL;
        private ObservableCollection<Role> _roles;

        private Role _selectedRole;
        public RolesViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            _roleBLL = new RoleBLL();
            ResetRole();


            AddCategoryCommand = new RelayCommand<object>(param => AddRole());
            ModifyCategoryCommand = new RelayCommand<object>(param => ModifyRole());
            DeleteCategoryCommand = new RelayCommand<object>(param => DeleteRole());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetRole());
        }

        private void DeleteRole()
        {
            try
            {
                _roleBLL.DeleteRoleWithId(SelectedRole.id);
                ResetRole();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyRole()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedRole.name))
                {
                    throw new Exception("All fields must be filled.");
                }
                Role newRole = new Role
                {
                    id = SelectedRole.id,
                    name = SelectedRole.name,
                };
                _roleBLL.ModifyRole(newRole);
                ResetRole();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddRole()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedRole.name))
                {
                    throw new Exception("All fields must be filled.");
                }
                Role newRole = new Role
                {
                    name = SelectedRole.name,
                };
                _roleBLL.AddRole(newRole);
                ResetRole();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetRole()
        {
            Roles = _roleBLL.GetRoles();
            SelectedRole = new Role();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand ModifyCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }
        public ObservableCollection<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                OnPropertyChanged(nameof(SelectedRole.name));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }

        public bool ModifyAndDeleteButtonsAreEnabled => SelectedRole != null && SelectedRole.id != 0;
        public bool AddButtonIsEnabled => SelectedRole != null && SelectedRole.id == 0;
    }
}
