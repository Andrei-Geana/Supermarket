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
    public class CategoryStatisticsViewModel : ViewModelBase
    {
        public CategoryStatisticsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProviderStatisticsViewModel> createProviderStatisticsMenu,
            Func<StatisticsViewModel> createEarningsMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProviderStatistics = new NavigationCommand(navigation, createProviderStatisticsMenu);
            NavigateToEarningsStatistics = new NavigationCommand(navigation, createEarningsMenu);
            ResetSelection();
        }

        private void ResetSelection()
        {
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProviderStatistics { get; set; }
        public ICommand NavigateToEarningsStatistics { get; set; }
    }
}
