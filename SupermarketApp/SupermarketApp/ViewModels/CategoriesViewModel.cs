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

namespace SupermarketApp.ViewModels
{
    public class CategoriesViewModel : ViewModelBase
    {
        private ProductCategoryBLL _productCategoryBLL;
        private ObservableCollection<Product_Category> _categories;

        private Product_Category _selectedCategory;
        public CategoriesViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            _productCategoryBLL = new ProductCategoryBLL();
            Categories = _productCategoryBLL.GetCategories();
            SelectedCategory = new Product_Category();
        }
        public ICommand NavigateBackToMenu { get; set; }
        public ObservableCollection<Product_Category> Categories 
        {
            get => _categories; 
            set => _categories = value; 
        }
        public Product_Category SelectedCategory 
        { 
            get => _selectedCategory; 
            set => _selectedCategory = value; 
        }
    }
}
