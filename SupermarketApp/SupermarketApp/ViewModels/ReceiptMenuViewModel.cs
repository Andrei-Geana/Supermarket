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
    public class ReceiptMenuViewModel : ViewModelBase
    {
        private ReceiptBLL _receiptBLL;
        private ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result> _receipts;
        private GetReceiptsWithCashierNamesAndTotalAmount_Result _receiptWithCashierNames;
        private DateTime _selectedDate = DateTime.Now;
        public ReceiptMenuViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu)
        {
            NavigateBackToMenu = new NavigationCommand(navigation, createMainMenu);
            GetHighestReceiptOfTheDay = new RelayCommand<object>(param => GetHighest());
            _receiptBLL = new ReceiptBLL(); 
            ResetReceipt();
        }

        private void GetHighest()
        {
            SelectedReceipt = Receipts.OrderByDescending(receipt => receipt.total_amount).FirstOrDefault();
        }

        private void ResetReceipt()
        {
            Receipts = _receiptBLL.GetReceiptsWithCashierNames();
        }
        public ICommand NavigateBackToMenu { get; set; }
        public ICommand GetHighestReceiptOfTheDay { get; set; }
        public ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result> Receipts 
        {
            get 
            {
                ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result> finalResult = new ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result>();
                foreach (var item in _receipts)
                {
                    if (item.release_date.Date != SelectedDate.Date)
                        continue;
                    finalResult.Add(item);
                }
                return finalResult;
            } 
            set => _receipts = value; 
        }
        public GetReceiptsWithCashierNamesAndTotalAmount_Result SelectedReceipt 
        { 
            get => _receiptWithCashierNames;
            set { _receiptWithCashierNames = value; OnPropertyChanged(nameof(SelectedReceipt)); }
        }
        public DateTime SelectedDate 
        { 
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); OnPropertyChanged(nameof(Receipts)); }
        }
    }
}
