using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DAL;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.BLL
{
    public class EmployeeBLL
    {
        private readonly EmployeeDAL _employeeDAL = new EmployeeDAL();

        public DataTable GetAllEmployees()
        {
            return _employeeDAL.GetAllEmployees();
        }

        public string SaveEmployee(EmployeeDTO employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FullName))
                return "Employee name cannot be empty.";

            if (!string.IsNullOrEmpty(employee.Email) && !Regex.IsMatch(employee.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Invalid email format.";

            bool result = (employee.EmployeeID == 0)
                ? _employeeDAL.AddEmployee(employee)
                : _employeeDAL.UpdateEmployee(employee);

            return result ? "" : "Failed to save the employee.";
        }

        public bool DeleteEmployee(int employeeId)
        {
            // Note: You should check if the employee has an account before deleting.
            // This is a simplified version.
            return _employeeDAL.DeleteEmployee(employeeId);
        }
        public DataTable GetEmployeesWithoutAccount()
        {
            return _employeeDAL.GetEmployeesWithoutAccount();
        }
    }
}
