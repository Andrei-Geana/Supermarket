using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class UserBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();

        public UserBLL() { }

        public User Login(User user)
        {
            User u = entities.Users.Where(mock => mock.password == user.password).Where(mock => mock.name == user.name).FirstOrDefault();
            return u;
        }
    }
}
