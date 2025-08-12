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
    public class ProductDAL
    {
        // Get all products with their category name
        public DataTable GetAllProducts()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        P.ProductID, P.ProductName, P.UnitPrice, P.Quantity, C.CategoryName 
                    FROM Products P
                    LEFT JOIN Categories C ON P.CategoryID = C.CategoryID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool AddProduct(ProductDTO product)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Products (ProductName, UnitPrice, Quantity, CategoryID) VALUES (@Name, @Price, @Qty, @CatID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Qty", product.Quantity);
                // Handle possible null CategoryID
                cmd.Parameters.AddWithValue("@CatID", (object)product.CategoryID ?? System.DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateProduct(ProductDTO product)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Products SET ProductName = @Name, UnitPrice = @Price, Quantity = @Qty, CategoryID = @CatID WHERE ProductID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Qty", product.Quantity);
                cmd.Parameters.AddWithValue("@CatID", (object)product.CategoryID ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", product.ProductID);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                // WARNING: This will fail if the product is used in an Order. Proper handling is needed.
                string query = "DELETE FROM Products WHERE ProductID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", productId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
