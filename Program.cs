using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreManagementSystem_Trung.GUI;

namespace StoreManagementSystem_Trung
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bước 1: Tạo một instance của form Login
            frmLogin loginForm = new frmLogin();

            // Bước 2: Hiển thị form Login dưới dạng một hộp thoại.
            // Luồng chương trình sẽ tạm dừng ở đây cho đến khi loginForm được đóng.
            DialogResult result = loginForm.ShowDialog();

            // Bước 3: Kiểm tra kết quả trả về từ form Login.
            // Trong frmLogin.cs, chúng ta đã đặt this.DialogResult = DialogResult.OK khi đăng nhập thành công.
            if (result == DialogResult.OK)
            {
                // Nếu đăng nhập thành công, chạy form Main.
                // Application.Run() sẽ giữ cho ứng dụng tiếp tục chạy cho đến khi frmMain được đóng.
                Application.Run(new frmMain());
            }
            // Nếu người dùng nhấn Cancel trên form Login (hoặc đóng bằng dấu X),
            // DialogResult sẽ không phải là OK, và hàm Main() sẽ kết thúc,
            // ứng dụng sẽ thoát một cách hợp lý.
        }
    }
}
