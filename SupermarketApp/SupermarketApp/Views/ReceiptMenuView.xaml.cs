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
    /// Interaction logic for ReceiptMenuView.xaml
    /// </summary>
    public partial class ReceiptMenuView : UserControl
    {
        public ReceiptMenuView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ReceiptMenuViewModel;
            if (viewModel != null)
            {
                if (viewModel.SelectedReceipt == null)
                {
                    MessageBox.Show("No receipt was selected.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }
                if (viewModel.SelectedReceipt.id_receipt != 0)
                {
                    ReceiptView receiptView = new ReceiptView() { DataContext = new ReceiptViewModel(viewModel.SelectedReceipt.id_receipt) };
                    receiptView.Show();
                    return;

                }
                MessageBox.Show("Unable to open receipt.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ReceiptMenuViewModel;
            if (viewModel != null)
            {
                //get highest in selectedReceipt
                viewModel.GetHighestReceiptOfTheDay.Execute(null);
                if (viewModel.SelectedReceipt == null)
                {
                    MessageBox.Show("No receipt was found for the day.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
