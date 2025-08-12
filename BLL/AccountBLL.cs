using System;
using System.Collections.Generic;
using System.Data;
using BCrypt.Net;
using static BCrypt.Net.BCrypt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DAL;
using System.Security.Cryptography;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.BLL
{
    public class AccountBLL
    {
        private readonly AccountDAL _accountDAL = new AccountDAL();

        public bool Login(string username, string password, out string fullName, out string roleName, out int employeeId)
        {
            fullName = string.Empty;
            roleName = string.Empty;
            employeeId = 0;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            DataTable accountInfo = _accountDAL.CheckLogin(username, password);

            if (accountInfo != null)
            {
                // Nếu DAL trả về dữ liệu, tức là đăng nhập thành công
                fullName = accountInfo.Rows[0]["FullName"].ToString();
                roleName = accountInfo.Rows[0]["RoleName"].ToString();
                employeeId = Convert.ToInt32(accountInfo.Rows[0]["EmployeeID"]);
                return true;
            }
            return false;

            
        }

        public string CreateAccount(AccountDTO account)
        {
            if (string.IsNullOrWhiteSpace(account.Username))
                return "Username cannot be empty.";
            if (account.EmployeeID == 0)
                return "Please select an employee.";
            if (account.RoleID == 0)
                return "Please select a role.";

            // Mật khẩu mặc định bây giờ là văn bản thuần túy
            account.PasswordHash = "123456";

            if (_accountDAL.CreateAccount(account))
            {
                return ""; // Success
            }
            return "Failed to create account.";
        }

        public string ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                return "Old password and new password cannot be empty.";
            }

            // 1. Xác thực mật khẩu cũ bằng cách thử đăng nhập
            DataTable accountInfo = _accountDAL.CheckLogin(username, oldPassword);
            if (accountInfo == null)
            {
                return "Incorrect old password.";
            }

            // 2. Nếu mật khẩu cũ đúng, gọi DAL để cập nhật mật khẩu mới
            if (_accountDAL.ChangePassword(username, newPassword))
            {
                return ""; // Success
            }
            else
            {
                return "Failed to update password in the database.";
            }
        }
    }
}
