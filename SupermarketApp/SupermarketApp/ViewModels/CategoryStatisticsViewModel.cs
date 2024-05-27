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
    public class CategoryStatisticsViewModel : ViewModelBase
    {
        private ProductCategoryBLL _productCategoryBLL;
        private ObservableCollection<GetCategoryValue_Result> _results;
        public CategoryStatisticsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProviderStatisticsViewModel> createProviderStatisticsMenu,
            Func<StatisticsViewModel> createEarningsMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProviderStatistics = new NavigationCommand(navigation, createProviderStatisticsMenu);
            NavigateToEarningsStatistics = new NavigationCommand(navigation, createEarningsMenu);

            _productCategoryBLL = new ProductCategoryBLL();

            ResetSelection();
        }

        private void ResetSelection()
        {
            Results = _productCategoryBLL.GetCategoryStatistics();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProviderStatistics { get; set; }
        public ICommand NavigateToEarningsStatistics { get; set; }
        public ObservableCollection<GetCategoryValue_Result> Results { get => _results; set => _results = value; }
    }
}
