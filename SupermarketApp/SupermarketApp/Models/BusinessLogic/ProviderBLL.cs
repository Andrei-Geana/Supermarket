using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class ProviderBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        private ObservableCollection<Provider> _providers;

        public ProviderBLL()
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var products = entities.Providers.ToList();
            _providers = new ObservableCollection<Provider>();
            foreach (var product in products)
            {
                _providers.Add(product);
            }
        }

        ObservableCollection<Provider> GetProviders()
        {
            ReinitializeList();
            return _providers;
        }
    }
}
