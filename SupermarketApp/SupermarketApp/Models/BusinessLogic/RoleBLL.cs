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
            foreach (var role in roles) 
            {
                Role newRole = new Role
                {
                    id = role.id,
                    name = role.name
                };
                returnedRoles.Add(newRole);
            }
            return returnedRoles;
        }

        public int GetIdOfRole(string roleName)
        {
            return entities.Roles.Where(role => role.name.Equals(roleName)).FirstOrDefault().id;
        }

        public void AddRole(Role newRole)
        {
            try
            {
                entities.Roles.Add(newRole);
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Role was not added to database.");
            }
        }

        public void DeleteRoleWithId(int id)
        {
            try
            {
                //need to update with only logic deletion
                entities.Roles.Remove(entities.Roles.Where(role => role.id == id).FirstOrDefault());
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Role to be deleted was not found in database.");
            }
        }

        public void ModifyRole(Role newRole)
        {
            try
            {
                var existingCategory = entities.Roles.FirstOrDefault(role => role.id == newRole.id) ?? throw new Exception("Role not found in database");
                existingCategory.name = newRole.name;
                entities.SaveChanges();
            }
            catch
            {
                throw new Exception("Role was not modified in database.");
            }
        }
    }
}
