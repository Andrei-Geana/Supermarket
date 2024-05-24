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

namespace SupermarketApp.ViewModels
{
    public class ProvidersViewModel : ViewModelBase
    {
        private ProviderBLL _providerBLL;
        private ObservableCollection<Provider> _providers;
        private Provider _selectedProvider;
        public ProvidersViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            _providerBLL = new ProviderBLL();
            ResetProvider();

            AddProviderCommand = new RelayCommand<object>(param => AddProvider());
            ModifyProviderCommand = new RelayCommand<object>(param => ModifyProvider());
            DeleteProviderCommand = new RelayCommand<object>(param => DeleteProvider());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetProvider());
        }
        public ObservableCollection<Provider> Providers 
        { 
            get => _providers;
            set
            {
                _providers = value;
                OnPropertyChanged(nameof(Providers));
            }        
        }
        public Provider SelectedProvider 
        { 
            get => _selectedProvider;
            set 
            { 
                _selectedProvider = value;
                OnPropertyChanged(nameof(SelectedProvider));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }
        private void ResetProvider()
        {
            Providers = _providerBLL.GetProviders();
            SelectedProvider = new Provider();
        }

        private void DeleteProvider()
        {
            try
            {
                _providerBLL.DeleteProviderWithId(SelectedProvider.id);
                ResetProvider();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyProvider()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedProvider.name) || string.IsNullOrEmpty(SelectedProvider.country_of_origin))
                {
                    throw new Exception("All fields must be filled.");
                }
                Provider newRole = new Provider
                {
                    id = SelectedProvider.id,
                    name = SelectedProvider.name,
                    country_of_origin = SelectedProvider.country_of_origin,
                };
                _providerBLL.ModifyProvider(newRole);
                ResetProvider();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProvider()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedProvider.name) || string.IsNullOrEmpty(SelectedProvider.country_of_origin))
                {
                    throw new Exception("All fields must be filled.");
                }
                Provider newProvider = new Provider
                {
                    name = SelectedProvider.name,
                    country_of_origin = SelectedProvider.country_of_origin,
                };
                _providerBLL.AddProvider(newProvider);
                ResetProvider();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand AddProviderCommand { get; set; }
        public ICommand ModifyProviderCommand { get; set; }
        public ICommand DeleteProviderCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }
        public bool ModifyAndDeleteButtonsAreEnabled => SelectedProvider != null && SelectedProvider.id != 0;
        public bool AddButtonIsEnabled => SelectedProvider != null && SelectedProvider.id == 0;
    }
}
