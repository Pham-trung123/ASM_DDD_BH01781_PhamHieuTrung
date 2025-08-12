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
    public class CustomerDAL
    {
        public DataTable GetAllCustomers()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT CustomerID, FullName, Phone, Address FROM Customers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool AddCustomer(CustomerDTO customer)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Customers (FullName, Phone, Address) VALUES (@FullName, @Phone, @Address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                cmd.Parameters.AddWithValue("@Phone", (object)customer.Phone ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? System.DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateCustomer(CustomerDTO customer)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Customers SET FullName = @FullName, Phone = @Phone, Address = @Address WHERE CustomerID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                cmd.Parameters.AddWithValue("@Phone", (object)customer.Phone ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", customer.CustomerID);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Customers WHERE CustomerID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", customerId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
