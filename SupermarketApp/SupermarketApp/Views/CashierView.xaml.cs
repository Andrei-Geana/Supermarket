using SupermarketApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupermarketApp.Views
{
    /// <summary>
    /// Interaction logic for CashierView.xaml
    /// </summary>
    public partial class CashierView : UserControl
    {
        public CashierView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CashierViewModel;
            if (viewModel != null)
            {
                viewModel.CreateReceiptButton.Execute(null);
                int id = viewModel.IdOfNewReceipt;
                if(id==-1)
                {
                    return;
                }
                ReceiptView receiptView = new ReceiptView() { DataContext = new ReceiptViewModel(id) };
                receiptView.Show();
            }
        }
    }
}
