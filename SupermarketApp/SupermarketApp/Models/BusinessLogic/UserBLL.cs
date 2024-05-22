using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class UserBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();

        public UserBLL() {}

        public User Login(User user)
        {
            User u = entities.Users.Where(mock => mock.password == user.password).Where(mock => mock.name == user.name).FirstOrDefault();
            return u;
        }

        public ObservableCollection<GetUsers_Result> GetUsers()
        {
            var users = entities.GetUsersWithRoles().ToList();
            ObservableCollection< GetUsers_Result > returnedUsers = new ObservableCollection< GetUsers_Result >();
            foreach (var user in users) { returnedUsers.Add(user); }
            return returnedUsers;
        }
    }
}
