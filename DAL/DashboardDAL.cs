using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem_Trung.DAL
{
    public class DashboardDAL
    {
        public int GetProductCount()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                // ExecuteScalar is efficient for queries that return a single value
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetCustomerCount()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Customers";
                SqlCommand cmd = new SqlCommand(query, conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public decimal GetTotalRevenueThisMonth()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT ISNULL(SUM(TotalAmount), 0) 
                    FROM Orders 
                    WHERE MONTH(OrderDate) = MONTH(GETDATE()) 
                    AND YEAR(OrderDate) = YEAR(GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return (result == DBNull.Value) ? 0 : Convert.ToDecimal(result);
            }
        }

        public DataTable GetTopSellingProducts()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                // Get top 5 products based on total quantity sold
                string query = @"
                    SELECT TOP 5
                        P.ProductName,
                        SUM(OD.Quantity) AS TotalQuantitySold
                    FROM OrderDetails OD
                    JOIN Products P ON OD.ProductID = P.ProductID
                    GROUP BY P.ProductName
                    ORDER BY TotalQuantitySold DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable GetSalesReport(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                O.OrderID,
                O.OrderDate,
                C.FullName AS CustomerName,
                E.FullName AS EmployeeName,
                O.TotalAmount
            FROM Orders O
            JOIN Customers C ON O.CustomerID = C.CustomerID
            JOIN Employees E ON O.EmployeeID = E.EmployeeID
            WHERE O.OrderDate BETWEEN @StartDate AND @EndDate
            ORDER BY O.OrderDate ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                // We add 1 day to endDate and check for < to include the entire end day.
                cmd.Parameters.AddWithValue("@StartDate", startDate.Date);
                cmd.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
