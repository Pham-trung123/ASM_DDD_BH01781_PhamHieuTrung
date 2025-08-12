using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DAL;

namespace StoreManagementSystem_Trung.BLL
{
    public class RoleBLL
    {
        private readonly RoleDAL _roleDAL = new RoleDAL();
        public DataTable GetAllRoles()
        {
            return _roleDAL.GetAllRoles();
        }
    }
}
