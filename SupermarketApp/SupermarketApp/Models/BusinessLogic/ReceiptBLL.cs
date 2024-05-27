using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class ReceiptBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        private ObservableCollection<Receipt> _receipts;

        public ReceiptBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var receipts = entities.Receipts.ToList();
            _receipts = new ObservableCollection<Receipt>();
            foreach (var receipt in receipts)
            {
                _receipts.Add(receipt);
            }
        }

        public int InsertReceiptAndGetId(Receipt receipt)
        {
            try
            {
                int? id = entities.InsertReceiptAndGetId(receipt.id_cashier, receipt.release_date, receipt.received_amount).FirstOrDefault();
                return id == null ? throw new Exception() : id ?? -1;
            }
            catch
            {
                throw new Exception("Receipt was not added to database");
            }
        }

        public Receipt GetReceiptWithId(int id)
        {
            try
            {
                return _receipts.Where(receipt => receipt.id_receipt == id).FirstOrDefault();
            }
            catch
            {
                throw new Exception("Receipt was not found in database.");
            }
        }

        public GetReceiptWithUsername_Result GetReceiptWithCashierNameWithId(int id)
        {
            try
            {
                return entities.GetReceiptWithUsername(id).First();
            }
            catch
            {
                throw new Exception("Receipt was not found in database.");
            }
        }

        public ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result> GetReceiptsWithCashierNames()
        {
            var data = entities.GetReceiptsWithCashierNamesAndTotalAmount().ToList();
            ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result> result = new ObservableCollection<GetReceiptsWithCashierNamesAndTotalAmount_Result>();
            foreach (var item in data) result.Add(item);
            return result;
        }

        public ObservableCollection<GetReceiptMonthlyStatistics_Result> GetSumOfReceiptsPerDays()
        {
            var data = entities.GetReceiptMonthlyStatistics().ToList().OrderBy(item => item.release_date);
            ObservableCollection<GetReceiptMonthlyStatistics_Result> result = new ObservableCollection<GetReceiptMonthlyStatistics_Result>();
            foreach (var item in data) result.Add(item);
            return result;
        }
    }
}
