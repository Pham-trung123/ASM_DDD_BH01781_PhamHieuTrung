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
    public class AccountDAL
    {
        /// <summary>
        /// Kiểm tra username và password trực tiếp trong CSDL.
        /// </summary>
        /// <returns>DataTable chứa thông tin người dùng nếu khớp, ngược lại trả về null.</returns>
        public DataTable CheckLogin(string username, string password)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        E.FullName, 
                        R.RoleName,
                        A.EmployeeID
                    FROM Accounts A
                    JOIN Employees E ON A.EmployeeID = E.EmployeeID
                    JOIN Roles R ON A.RoleID = R.RoleID
                    WHERE A.Username = @Username AND A.PasswordHash = @Password"; // So sánh trực tiếp

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt.Rows.Count > 0 ? dt : null;
            }
        }

        /// <summary>
        /// Tạo tài khoản mới với mật khẩu văn bản thuần túy.
        /// </summary>
        public bool CreateAccount(AccountDTO account)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Accounts (Username, PasswordHash, RoleID, EmployeeID) VALUES (@Username, @Password, @RoleID, @EmployeeID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", account.Username);
                cmd.Parameters.AddWithValue("@Password", account.PasswordHash); // PasswordHash bây giờ chứa mật khẩu thật
                cmd.Parameters.AddWithValue("@RoleID", account.RoleID);
                cmd.Parameters.AddWithValue("@EmployeeID", account.EmployeeID);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Thay đổi mật khẩu với mật khẩu văn bản thuần túy.
        /// </summary>
        public bool ChangePassword(string username, string newPassword)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Accounts SET PasswordHash = @NewPassword WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@Username", username);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}