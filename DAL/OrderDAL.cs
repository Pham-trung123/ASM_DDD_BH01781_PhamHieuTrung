using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.DAL
{
    public class OrderDAL
    {
        public bool CreateOrder(OrderDTO order)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Step 1: Insert into Orders table and get the new OrderID
                    string orderQuery = @"
                        INSERT INTO Orders (OrderDate, CustomerID, EmployeeID, TotalAmount) 
                        VALUES (@OrderDate, @CustomerID, @EmployeeID, @TotalAmount);
                        SELECT SCOPE_IDENTITY();";

                    SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                    orderCmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    orderCmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    orderCmd.Parameters.AddWithValue("@EmployeeID", order.EmployeeID);
                    orderCmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

                    int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());

                    // Step 2: Loop through order details and insert them
                    foreach (var detail in order.OrderDetails)
                    {
                        string detailQuery = @"
                            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice) 
                            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice)";

                        SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction);
                        detailCmd.Parameters.AddWithValue("@OrderID", orderId);
                        detailCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                        detailCmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                        detailCmd.Parameters.AddWithValue("@UnitPrice", detail.Price); // Use UnitPrice from CartItem
                        detailCmd.ExecuteNonQuery();

                        // Step 3: Update product stock
                        string updateProductQuery = "UPDATE Products SET Quantity = Quantity - @SoldQuantity WHERE ProductID = @ProductID";
                        SqlCommand updateProductCmd = new SqlCommand(updateProductQuery, conn, transaction);
                        updateProductCmd.Parameters.AddWithValue("@SoldQuantity", detail.Quantity);
                        updateProductCmd.Parameters.AddWithValue("@ProductID", detail.ProductID);
                        updateProductCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    // Optionally, log the exception here
                    return false;
                }
            }
        }
        public DataTable GetAllOrders()
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
            ORDER BY O.OrderDate DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetOrderDetailsByOrderId(int orderId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT 
                P.ProductName,
                OD.Quantity,
                OD.UnitPrice
            FROM OrderDetails OD
            JOIN Products P ON OD.ProductID = P.ProductID
            WHERE OD.OrderID = @OrderID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
