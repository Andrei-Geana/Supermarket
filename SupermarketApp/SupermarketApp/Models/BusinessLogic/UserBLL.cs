﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketApp.Models.BusinessLogic
{
    public class UserBLL
    {
        private SupermarketMAPEntities entities = new SupermarketMAPEntities();
        private ObservableCollection<User> _users;
        public UserBLL() 
        {
            ReinitializeList();
        }

        public User Login(User user)
        {
            User u = _users.Where(mock => mock.password == user.password).Where(mock => mock.name == user.name).FirstOrDefault();
            return u;
        }

        public void ReinitializeList()
        {
            var users = entities.Users.Where(item => item.deleted == false).ToList();
            _users = new ObservableCollection<User>();
            foreach (var user in users) { _users.Add(user); }
        }

        public ObservableCollection<GetUsers_Result> GetUsersWithRoleName()
        {
            ObservableCollection<GetUsers_Result> returnedUsers = new ObservableCollection<GetUsers_Result>();
            var users = entities.GetUsers().ToList();
            foreach (var user in users) 
            {
                returnedUsers.Add(user); 
            }
            return returnedUsers;
        }

        public void DeleteUserWithId(int ID)
        {
            try
            {
                //need to update with only logic deletion
                //entities.Users.Remove(entities.Users.Where(user => user.id == ID).FirstOrDefault());
                entities.Users.Where(user => user.id == ID).FirstOrDefault().deleted = true;
                entities.SaveChanges();
                _users.Remove(_users.Where(user => user.id == ID).FirstOrDefault());
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                ReinitializeList();
                throw new Exception ( "User to be deleted was not found in database." ); 
            }
        }

        public void AddUser(User newUser)
        {
            try
            {
                entities.InsertUser(newUser.name, newUser.password, newUser.id_role);
                entities.SaveChanges();
                _users.Add(newUser);
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                ReinitializeList();
                throw new Exception("User was not added to database.");
            }
        }

        public void ModifyUser(User userToBeModified)
        {
            try
            {
                entities.UpdateUser(userToBeModified.id, userToBeModified.name, userToBeModified.password, userToBeModified.id_role);
                entities.SaveChanges();

                var currentUser = _users.FirstOrDefault(user => user.id == userToBeModified.id);
                currentUser.name = userToBeModified.name;
                currentUser.password = userToBeModified.password;
                currentUser.id_role = userToBeModified.id_role;

            }
            catch
            {
                entities = new SupermarketMAPEntities();
                ReinitializeList();
                throw new Exception("User was not modified in database.");
            }
        }
    }
}
