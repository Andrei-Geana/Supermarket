using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class ReceiptDetailBLL
    {
        private SupermarketMAPEntities1 entities = new SupermarketMAPEntities1();
        private ObservableCollection<Receipt_Details> _receipts;

        public ReceiptDetailBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var receipts = entities.Receipt_Details.ToList();
            _receipts = new ObservableCollection<Receipt_Details>();
            foreach (var receipt in receipts)
            {
                _receipts.Add(receipt);
            }
        }

        public void InsertReceiptDetails(Receipt_Details receipt)
        {
            try
            {
                entities.Receipt_Details.Add(receipt);
                entities.SaveChanges();
                _receipts.Add(receipt);
            }
            catch
            {
                throw new Exception("Receipt detail was not added to database");
            }
        }

        public ObservableCollection<GetReceiptDetailsByReceiptId_Result> GetDetailsOfReceiptId(int id)
        {
            ObservableCollection<GetReceiptDetailsByReceiptId_Result> returnedDetails = new ObservableCollection<GetReceiptDetailsByReceiptId_Result>();
            var details = entities.GetReceiptDetailsByReceiptId(id).ToList();
            foreach (var detail in details)
            {
                returnedDetails.Add(detail);
            }
            return returnedDetails;
        }
    }
}
