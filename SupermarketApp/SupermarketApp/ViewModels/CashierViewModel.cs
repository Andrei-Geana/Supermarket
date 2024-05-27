using Checkers_Game.Command;
using SupermarketApp.Commands;
using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using SupermarketApp.Stores;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SupermarketApp.ViewModels
{
    public class CashierViewModel : ViewModelBase
    {
        private RoleBLL _roleBLL;
        private StockBLL _stockBLL;
        private ProductCategoryBLL _categoryBLL;
        private ProviderBLL _providerBLL;
        private ReceiptBLL _receiptBLL;
        private ReceiptDetailBLL _receiptDetailBLL;

        private ObservableCollection<Product_Category> _categories;
        private ObservableCollection<Provider> _providers;
        private ObservableCollection<GetRemainingStock_Result> _stocks;
        private GetRemainingStock_Result _selectedStock;

        private string _barcode = "";
        private string _name = "";
        private string _provider = "";
        private string _category = "";
        private int _quantity;
        private double _receivedAmount;
        private double _total;
        private DateTime _expirationDate = DateTime.Now;

        public int IdOfNewReceipt {  get; set; }
        public CashierViewModel(Navigation navigation, Func<MainMenuViewModel> createMainMenu, Func<LoginViewModel> createLoginMenu)
        {
            NavigateBackToLogin = new NavigationCommand(navigation, createLoginMenu);
            NavigateBackToMainMenu = new NavigationCommand(navigation, createMainMenu);
            NavigateBack = new RelayCommand<object>(param => DecideWhereToGoBack());
            AddProductToReceipt = new RelayCommand<object>(param => AddProductToReceiptDetails());
            RemoveProductFromReceipt = new RelayCommand<object>(param => RemoveFromReceiptDetails());
            CreateReceiptButton = new RelayCommand<object>(param => CreateReceipt());

            _stockBLL = new StockBLL();
            _roleBLL = new RoleBLL();
            _categoryBLL = new ProductCategoryBLL();
            _providerBLL = new ProviderBLL();
            _receiptBLL = new ReceiptBLL();
            _receiptDetailBLL = new ReceiptDetailBLL();

            Categories = _categoryBLL.GetCategories();
            Providers = _providerBLL.GetProviders();
            ReceiptDetails = new ObservableCollection<GetReceiptDetailsWithProductNames_Result>();

            Categories.Insert(0, new Product_Category() { name = "" });
            Providers.Insert(0, new Provider() { name = "" });
            ResetSelection();


        }

        private void CreateReceipt()
        {
            try
            {
                IdOfNewReceipt = -1;
                if (Total > ReceivedAmount)
                {
                    throw new Exception("Invalid data: Received amount is not enough.");
                }

                Receipt receipt = new Receipt() { release_date = DateTime.Now, id_cashier = App.CurrentUser.id, received_amount = ReceivedAmount };
                IdOfNewReceipt = _receiptBLL.InsertReceiptAndGetId(receipt);
                if(IdOfNewReceipt == -1)
                {
                    throw new Exception("Error: Unable to save receipt.");
                }
                foreach(var detail in ReceiptDetails)
                {
                    detail.id_receipt = IdOfNewReceipt;
                    _receiptDetailBLL.InsertReceiptDetails(detail);
                }

                SaveRemainingStock();
                ResetSelection();
                ReceiptDetails = new ObservableCollection<GetReceiptDetailsWithProductNames_Result>();
                Quantity = 0;
                ReceivedAmount = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveRemainingStock()
        {
            try
            {
                foreach (var item in _stocks)
                {
                    GetStockDetails_Result stock = new GetStockDetails_Result()
                    {
                        id = item.id,
                        remaining_quantity = item.remaining_quantity,
                        sell_price=item.sell_price,
                    };
                    _stockBLL.ModifyStock(stock);
                }
            }
            catch
            {
                throw new Exception("Error: Unable to modify stock.");
            }
        }

        private void RemoveFromReceiptDetails()
        {
            try
            {
                if (Quantity <= 0 || Quantity > SelectedReceiptDetail.quantity)
                {
                    throw new Exception("Invalid data: Quantity invalid to remove.");
                }
                _stocks.Where(stock => stock.id == SelectedReceiptDetail.id_stock).FirstOrDefault().remaining_quantity += Quantity;
                SelectedReceiptDetail.quantity -= Quantity;
                if (SelectedReceiptDetail.quantity == 0)
                {
                    ReceiptDetails.Remove(SelectedReceiptDetail);
                }
                else
                {
                    var updatedDetails = new ObservableCollection<GetReceiptDetailsWithProductNames_Result>(_receiptDetails);
                    ReceiptDetails = updatedDetails;
                }
                var updatedStocks = new ObservableCollection<GetRemainingStock_Result>(_stocks);
                Stocks = updatedStocks;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                OnPropertyChanged(nameof(CreateReceiptButtonIsEnabled));
                OnPropertyChanged(nameof(Total));
            }
        }

        private ObservableCollection<GetReceiptDetailsWithProductNames_Result> _receiptDetails;
        private GetReceiptDetailsWithProductNames_Result _selectedReceiptDetail;

        private void AddProductToReceiptDetails()
        {
            OnPropertyChanged(nameof(CreateReceiptButtonIsEnabled));
            try
            {
                if(Quantity<=0 || Quantity>SelectedStock.remaining_quantity)
                {
                    throw new Exception("Invalid data: Quantity invalid.");
                }
                GetReceiptDetailsWithProductNames_Result details = new GetReceiptDetailsWithProductNames_Result()
                {
                    id_stock = SelectedStock.id,
                    price_per_item = SelectedStock.sell_price,
                    quantity = Quantity,
                    product_name = SelectedStock.product_name

                };
                var alreadyIn = ReceiptDetails.Where(x => x.id_stock == details.id_stock).FirstOrDefault();
                if(alreadyIn!=null)
                {
                    alreadyIn.quantity += details.quantity;
                    var updatedDetails = new ObservableCollection<GetReceiptDetailsWithProductNames_Result>(_receiptDetails);
                    ReceiptDetails = updatedDetails;
                }
                else
                    ReceiptDetails.Add(details);
                Stocks.Where(stock => stock.id == details.id_stock).FirstOrDefault().remaining_quantity -= Quantity;
                var updatedStocks = new ObservableCollection<GetRemainingStock_Result>(_stocks);
                Stocks = updatedStocks;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                OnPropertyChanged(nameof(CreateReceiptButtonIsEnabled)); 
                OnPropertyChanged(nameof(Total));
            }
        }

        public ICommand NavigateBackToLogin { get; set; }
        public ICommand NavigateBackToMainMenu { get; set; }
        public ICommand NavigateBack { get; set; }
        public ICommand AddProductToReceipt { get; set; }
        public ICommand RemoveProductFromReceipt { get; set; }
        public ICommand CreateReceiptButton { get; set; }

        public void ResetSelection()
        {
            Stocks = _stockBLL.GetRemainingStock();
            SelectedStock = new GetRemainingStock_Result();
        }


        public ObservableCollection<GetRemainingStock_Result> Stocks 
        { 
            get
            {
                ObservableCollection<GetRemainingStock_Result> finalResult = new ObservableCollection<GetRemainingStock_Result>();
                foreach(var item in _stocks)
                {
                    if(finalResult.Where(itemInResultAlready => itemInResultAlready.product_id ==  item.product_id).Any())
                            continue;
                    if (item.remaining_quantity == 0)
                        continue;
                    if (!item.bar_code.StartsWith(Barcode))
                        continue;
                    if (!item.product_name.StartsWith(Name))
                        continue;
                    if (!string.IsNullOrEmpty(Provider) && item.provider_name != Provider)
                        continue;
                    if (!string.IsNullOrEmpty(Category) && item.category_name != Category)
                        continue;
                    if (item.expiration_date < ExpirationDate)
                        continue;
                    finalResult.Add(item);
                }
                return finalResult;
            }
            set { _stocks = value; OnPropertyChanged(nameof(Stocks)); }
        }
        public GetRemainingStock_Result SelectedStock { get => _selectedStock; set { _selectedStock = value; OnPropertyChanged(nameof(SelectedStock)); OnPropertyChanged(nameof(AddButtonIsEnabled)); } }

        public string Barcode 
        { 
            get => _barcode; 
            set 
            { 
                if (value.Length > 8) 
                    return; 
                _barcode = value; 
                OnPropertyChanged(nameof(Barcode)); 
                OnPropertyChanged(nameof(Stocks)); 
            } 
        }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(Stocks)); } }
        public string Provider { get => _provider; set { _provider = value; OnPropertyChanged(nameof(Provider)); OnPropertyChanged(nameof(Stocks)); } }
        public string Category { get => _category; set { _category = value; OnPropertyChanged(nameof(Category)); OnPropertyChanged(nameof(Stocks)); } }
        public DateTime ExpirationDate 
        { 
            get => _expirationDate; 
            set 
            {
                DateTime currentDay = DateTime.Now;
                if (value.Year < currentDay.Year ||
                (value.Year == currentDay.Year && value.Month < currentDay.Month) ||
                (value.Year == currentDay.Year && value.Month == currentDay.Month && value.Day < currentDay.Day))
                {
                    return;
                }
                _expirationDate = value; 
                OnPropertyChanged(nameof(ExpirationDate)); 
                OnPropertyChanged(nameof(Stocks)); 
            }
        }

        public ObservableCollection<Product_Category> Categories { get => _categories; set => _categories = value; }
        public ObservableCollection<Provider> Providers { get => _providers; set => _providers = value; }



        private void DecideWhereToGoBack()
        {
            //if current user is admin
            if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
            {
                NavigateBackToMainMenu.Execute(null);
            }
            else
            {
                NavigateBackToLogin.Execute(null);
            }
        }

        public string TextButton
        {
            get
            {
                if (App.CurrentUser.id_role == _roleBLL.GetIdOfAdmin())
                {
                    return "BACK";
                }
                else
                {
                    return "LOG OUT";
                }
            }
        }

        public bool MinusButtonIsEnabled => SelectedReceiptDetail != null && SelectedReceiptDetail.quantity != 0;
        public bool AddButtonIsEnabled => SelectedStock != null && SelectedStock.id != 0;
        public bool CreateReceiptButtonIsEnabled => ReceiptDetails.Count != 0 && ReceivedAmount>0;

        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(nameof(Quantity)); } }

        public ObservableCollection<GetReceiptDetailsWithProductNames_Result> ReceiptDetails { get => _receiptDetails; set { _receiptDetails = value; OnPropertyChanged(nameof(ReceiptDetails)); OnPropertyChanged(nameof(Total)); } }
        public GetReceiptDetailsWithProductNames_Result SelectedReceiptDetail { get => _selectedReceiptDetail; set { _selectedReceiptDetail = value; OnPropertyChanged(nameof(SelectedReceiptDetail)); OnPropertyChanged(nameof(MinusButtonIsEnabled)); } }

        public double ReceivedAmount
        {
            get => _receivedAmount;
            set
            {
                _receivedAmount = value;
                OnPropertyChanged(nameof(ReceivedAmount));
                OnPropertyChanged(nameof(CreateReceiptButtonIsEnabled));
            }
        }

        public double Total
        {
            get
            {
                double total = 0;
                foreach (var detail in _receiptDetails) { total += detail.quantity * detail.price_per_item; }
                return total;
            }
            set => _total=value;
        }
    }
}
