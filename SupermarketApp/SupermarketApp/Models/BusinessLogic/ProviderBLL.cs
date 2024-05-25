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
        private SupermarketMAPEntities1 entities = new SupermarketMAPEntities1();
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

        public ObservableCollection<Provider> GetProviders()
        {
            ReinitializeList();
            return _providers;
        }

        public void AddProvider(Provider newProvider)
        {
            try
            {
                entities.Providers.Add(newProvider);
                entities.SaveChanges();
                _providers.Add(newProvider);
            }
            catch
            {
                throw new Exception("Provider was not added to database.");
            }
        }

        public void DeleteProviderWithId(int id)
        {
            try
            {
                //need to update with only logic deletion
                entities.Providers.Remove(entities.Providers.Where(Provider => Provider.id == id).FirstOrDefault());
                entities.SaveChanges();
                _providers.Remove(_providers.Where(Provider => Provider.id == id).FirstOrDefault());
            }
            catch
            {
                throw new Exception("Provider to be deleted was not found in database.");
            }
        }

        public void ModifyProvider(Provider newProvider)
        {
            try
            {
                var existingCategory = entities.Providers.FirstOrDefault(Provider => Provider.id == newProvider.id) ?? throw new Exception("Provider not found in database");
                existingCategory.name = newProvider.name;
                entities.SaveChanges();
                var currentCategory = _providers.FirstOrDefault(Provider => Provider.id == newProvider.id);
                currentCategory.name = newProvider.name;
            }
            catch
            {
                throw new Exception("Provider was not modified in database.");
            }
        }
    }
}
