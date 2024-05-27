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
        private ObservableCollection<Role> _roles;

        public RoleBLL() 
        {
            ReinitializeList();
        }

        private void ReinitializeList()
        {
            var roles = entities.Roles.ToList();
            _roles = new ObservableCollection<Role>();
            foreach (var role in roles)
            {
                Role newRole = new Role
                {
                    id = role.id,
                    name = role.name
                };
                _roles.Add(newRole);
            }
        }

        public int GetIdOfAdmin()
        {
            return _roles.Where(role => role.name == _adminString).First().id;
        }

        public ObservableCollection<Role> GetRoles()
        {
            ReinitializeList();
            return _roles;
        }

        public int GetIdOfRole(string roleName)
        {
            return _roles.Where(role => role.name.Equals(roleName)).FirstOrDefault().id;
        }

        public string GetRoleName(int id)
        {
            return _roles.Where(role => role.id == id).FirstOrDefault().name;
        }

        public void AddRole(Role newRole)
        {
            try
            {
                entities.Roles.Add(newRole);
                entities.SaveChanges();
                _roles.Add(newRole);
            }
            catch
            {
                entities = new SupermarketMAPEntities();
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
                _roles.Remove(_roles.Where(role => role.id == id).FirstOrDefault());
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Role to be deleted was not found in database.");
            }
        }

        public void ModifyRole(Role newRole)
        {
            try
            {
                entities.UpdateRole(newRole.id, newRole.name);
                entities.SaveChanges();
                var currentCategory = _roles.FirstOrDefault(role => role.id == newRole.id);
                currentCategory.name = newRole.name;
            }
            catch
            {
                entities = new SupermarketMAPEntities();
                throw new Exception("Role was not modified in database.");
            }
        }
    }
}
