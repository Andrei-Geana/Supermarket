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
    public class CategoriesViewModel : ViewModelBase
    {
        private ProductCategoryBLL _productCategoryBLL;
        private ObservableCollection<Product_Category> _categories;

        private Product_Category _selectedCategory;
        public CategoriesViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProductsViewModel> createProductsMenu, Func<ProvidersViewModel> createProvidersMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProducts = new NavigationCommand(navigation, createProductsMenu);
            NavigateToProviders = new NavigationCommand(navigation, createProvidersMenu);
            _productCategoryBLL = new ProductCategoryBLL();
            ResetCategory();


            AddCategoryCommand = new RelayCommand<object>(param => AddCategory());
            ModifyCategoryCommand = new RelayCommand<object>(param => ModifyCategory());
            DeleteCategoryCommand = new RelayCommand<object>(param => DeleteCategory());
            ResetSelectionCommand = new RelayCommand<object>(param => ResetCategory());
        }

        private void DeleteCategory()
        {
            try
            {
                _productCategoryBLL.DeleteCategoryWithId(SelectedCategory.id);
                ResetCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifyCategory()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedCategory.name))
                {
                    throw new Exception("All fields must be filled.");
                }
                Product_Category newCategory = new Product_Category
                {
                    id = SelectedCategory.id,
                    name = SelectedCategory.name.ToLower(),
                };
                _productCategoryBLL.ModifyCategory(newCategory);
                ResetCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddCategory()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedCategory.name))
                {
                    throw new Exception("All fields must be filled.");
                }
                Product_Category newCategory = new Product_Category
                {
                    name = SelectedCategory.name.ToLower(),
                };
                _productCategoryBLL.AddCategory(newCategory);
                ResetCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetCategory()
        {
            Categories = _productCategoryBLL.GetCategories();
            SelectedCategory = new Product_Category();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProducts { get; set; }
        public ICommand NavigateToProviders { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand ModifyCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand ResetSelectionCommand { get; set; }
        public ObservableCollection<Product_Category> Categories 
        {
            get => _categories;
            set 
            { 
                _categories = value; 
                OnPropertyChanged(nameof(Categories));
            } 
        }
        public Product_Category SelectedCategory 
        { 
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                OnPropertyChanged(nameof(SelectedCategory.name));
                OnPropertyChanged(nameof(ModifyAndDeleteButtonsAreEnabled));
                OnPropertyChanged(nameof(AddButtonIsEnabled));
            }
        }

        public bool ModifyAndDeleteButtonsAreEnabled => SelectedCategory != null && SelectedCategory.id != 0;
        public bool AddButtonIsEnabled => SelectedCategory != null && SelectedCategory.id == 0;
    }
}
