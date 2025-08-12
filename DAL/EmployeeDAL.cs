using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.DAL
{
    public class EmployeeDAL
    {
        public DataTable GetAllEmployees()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT EmployeeID, FullName, Position, Phone, Email FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool AddEmployee(EmployeeDTO employee)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Employees (FullName, Position, Phone, Email) VALUES (@FullName, @Position, @Phone, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Position", (object)employee.Position ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)employee.Email ?? System.DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateEmployee(EmployeeDTO employee)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Employees SET FullName = @FullName, Position = @Position, Phone = @Phone, Email = @Email WHERE EmployeeID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Position", (object)employee.Position ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)employee.Email ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", employee.EmployeeID);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Employees WHERE EmployeeID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", employeeId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public DataTable GetEmployeesWithoutAccount()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT E.EmployeeID, E.FullName 
            FROM Employees E
            LEFT JOIN Accounts A ON E.EmployeeID = A.EmployeeID
            WHERE A.AccountID IS NULL";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
