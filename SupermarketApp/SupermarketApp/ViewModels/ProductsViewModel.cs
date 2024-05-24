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
    public class ProductsViewModel : ViewModelBase 
    {
        public ProductsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProvidersViewModel> createProvidersMenu, Func<CategoriesViewModel> createCategoriesMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProviders= new NavigationCommand(navigation, createProvidersMenu);
            NavigateToCategories= new NavigationCommand(navigation, createCategoriesMenu);

            AddProductCommand = new RelayCommand<object>(param => AddProduct());
            ModifyProductCommand = new RelayCommand<object>(param => ModifyProduct());
            DeleteProductCommand = new RelayCommand<object>(param => DeleteProduct());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetProduct());
        }

        private void ResetProduct()
        {
            throw new NotImplementedException();
        }

        private void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        private void ModifyProduct()
        {
            throw new NotImplementedException();
        }

        private void AddProduct()
        {
            throw new NotImplementedException();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProviders { get; set; }
        public ICommand NavigateToCategories { get; set; }
        public ICommand AddProductCommand { get; set; }
        public ICommand ModifyProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }
    }
}
