using System;
using System.Collections.Generic;
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
    }
}
