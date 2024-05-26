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
    public class ProviderStatisticsViewModel : ViewModelBase
    {
        public ProviderStatisticsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<StatisticsViewModel> createReceiptStatistics)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToEarningsStatistics = new NavigationCommand(navigation, createReceiptStatistics);
            ResetSelection();
        }

        private void ResetSelection()
        {
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToEarningsStatistics { get; set; }
    }
}
