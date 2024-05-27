using SupermarketApp.Models;
using SupermarketApp.Models.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.ViewModels
{
    public class ReceiptViewModel
    {
        private GetReceiptWithUsername_Result _receipt;
        private ObservableCollection<GetReceiptDetailsByReceiptId_Result> _receiptDetails;
        public ReceiptViewModel(int id)
        {
            ReceiptBLL receiptBLL = new ReceiptBLL();
            ReceiptDetailBLL receiptDetailBLL = new ReceiptDetailBLL();
            Receipt = receiptBLL.GetReceiptWithCashierNameWithId(id);
            ReceiptDetails = receiptDetailBLL.GetDetailsOfReceiptId(id);
        }

        public GetReceiptWithUsername_Result Receipt { get => _receipt; set => _receipt = value; }
        public ObservableCollection<GetReceiptDetailsByReceiptId_Result> ReceiptDetails { get => _receiptDetails; set => _receiptDetails = value; }

        public double TotalSum
        {
            get
            {
                double total = 0;
                foreach (var receipt in _receiptDetails) { total += receipt.quantity * receipt.price_per_item; }
                return total;
            }
        }
        public double RemainingAmount
        {
            get
            {
                return TotalSum - Receipt.received_amount;
            }
        }

    }
}
