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
using System.Windows.Input;
using System.Xml.Linq;

namespace SupermarketApp.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private ReceiptBLL _receiptBLL;
        private ObservableCollection<GetReceiptMonthlyStatistics_Result> _results;

        private DateTime _date = DateTime.Now;
        private string _userName = "";
        public StatisticsViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<ProviderStatisticsViewModel> createProviderStatisticsMenu,
            Func<CategoryStatisticsViewModel> createCategoryStatisticsMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateToProviderStatistics = new NavigationCommand(navigation, createProviderStatisticsMenu);
            NavigateToCategoryStatistics = new NavigationCommand(navigation, createCategoryStatisticsMenu);

            ResetSelection();
        }

        private void ResetSelection()
        {
            _receiptBLL = new ReceiptBLL();
            Results = _receiptBLL.GetSumOfReceiptsPerDays();
        }

        public ICommand NavigateBackToMenu { get; set; }
        public ICommand NavigateToProviderStatistics { get; set; }
        public ICommand NavigateToCategoryStatistics { get; set; }
        public ObservableCollection<GetReceiptMonthlyStatistics_Result> Results 
        {
            get 
            {
                ObservableCollection<GetReceiptMonthlyStatistics_Result> finalResult = new ObservableCollection<GetReceiptMonthlyStatistics_Result>();
                foreach (var item in _results)
                {
                    if (!string.IsNullOrEmpty(UserName) && !item.user_name.StartsWith(UserName.ToLower()))
                        continue;
                    if(item.release_date.Year != Date.Year)
                        continue;
                    if (item.release_date.Month != Date.Month)
                        continue;
                    finalResult.Add(item);
                }
                return finalResult;
            } 
            set => _results = value;
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(Results));
            }
        }
        public string UserName
        {
            get => _userName; set { _userName = value; OnPropertyChanged(nameof(UserName)); OnPropertyChanged(nameof(Results)); }
        }
    }
}
