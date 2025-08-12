using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem_Trung
{
    public static class UserSession
    {
        public static string Username { get; private set; }
        public static string FullName { get; private set; }
        public static string RoleName { get; private set; }
        public static int EmployeeID { get; private set; }

        public static void StartSession(string username, string fullName, string roleName, int employeeId)
        {
            Username = username;
            FullName = fullName;
            RoleName = roleName;
            EmployeeID = employeeId;
        }

        public static void EndSession()
        {
            Username = null;
            FullName = null;
            RoleName = null;
            EmployeeID = 0;
        }
    }
}
