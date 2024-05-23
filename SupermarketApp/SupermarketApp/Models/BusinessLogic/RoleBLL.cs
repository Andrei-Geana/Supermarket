using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class RoleBLL
    {
        private static readonly string _adminString = "ADMIN";
        private static readonly string _cashierString = "CASHIER";

        private SupermarketMAPEntities entities = new SupermarketMAPEntities();

        public RoleBLL() { }

        public int GetIdOfAdmin()
        {
            return entities.Roles.Where(role => role.name == _adminString).First().id;
        }

        public ObservableCollection<Role> GetRoles()
        {
            var roles = entities.Roles.ToList();
            ObservableCollection<Role> returnedRoles = new ObservableCollection<Role>();
            foreach (var role in roles) { returnedRoles.Add(role); }
            return returnedRoles;
        }

        public int GetIdOfRole(string roleName)
        {
            return entities.Roles.Where(role => role.name.Equals(roleName)).FirstOrDefault().id;
        }
    }
}
