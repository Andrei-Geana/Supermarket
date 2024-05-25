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
        private SupermarketMAPEntities1 entities = new SupermarketMAPEntities1();
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
    }
}
