using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreManagementSystem_Trung.GUI;

namespace StoreManagementSystem_Trung.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"User: {UserSession.FullName} | Role: {UserSession.RoleName}";
            ApplyPermissions();
            OpenDashboard();
        }

        private void ApplyPermissions()
        {
            string role = UserSession.RoleName;

            bool isAdmin = (role == "Admin");
            bool isSales = (role == "Sales");
            bool isWarehouse = (role == "Warehouse");

            // Management
            menuProductManagement.Visible = isAdmin || isWarehouse || isSales;
            menuCategoryManagement.Visible = isAdmin || isWarehouse;
            menuCustomerManagement.Visible = isAdmin || isSales;
            menuEmployeeManagement.Visible = isAdmin;
            menuAccountManagement.Visible = isAdmin;

            // Functions
            menuCreateNewOrder.Visible = isAdmin || isSales;
            // menuOrderHistory.Visible = isAdmin || isSales; // Uncomment if you have this menu item

            // System
            menuChangePassword.Visible = true;

            // Reports
            menuSalesReport.Visible = isAdmin;

            // Dashboard
            // menuDashboard.Visible = true; // Uncomment if you have this menu item
            bool canViewHistory = (UserSession.RoleName == "Admin" || UserSession.RoleName == "Sales");
            menuOrderHistory.Visible = canViewHistory;
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            // Close all MDI child forms before logging out
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            UserSession.EndSession();
            this.Hide();

            frmLogin loginForm = new frmLogin();
            DialogResult result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.Show();
                frmMain_Load(this, EventArgs.Empty);
            }
            else
            {
                this.Close();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void OpenDashboard()
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmDashboard)
                {
                    frm.Activate();
                    return;
                }
            }
            frmDashboard dashboardForm = new frmDashboard { MdiParent = this, WindowState = FormWindowState.Maximized };
            dashboardForm.Show();
        }

        // --- Menu Click Handlers ---

        private void menuDashboard_Click(object sender, EventArgs e)
        {
            OpenDashboard();
        }

        private void menuProductManagement_Click(object sender, EventArgs e)
        {
            frmProductManagement productForm = new frmProductManagement();
            productForm.ShowDialog();
        }

        private void menuEmployeeManagement_Click(object sender, EventArgs e)
        {
            frmEmployeeManagement employeeForm = new frmEmployeeManagement();
            employeeForm.ShowDialog();
        }

        private void menuCustomerManagement_Click(object sender, EventArgs e)
        {
            frmCustomerManagement customerForm = new frmCustomerManagement();
            customerForm.ShowDialog();
        }

        private void menuCategoryManagement_Click(object sender, EventArgs e)
        {
            frmCategoryManagement categoryForm = new frmCategoryManagement();
            categoryForm.ShowDialog();
        }

        private void menuCreateNewOrder_Click(object sender, EventArgs e)
        {
            frmSales salesForm = new frmSales();
            salesForm.ShowDialog();
        }

        private void menuAccountManagement_Click(object sender, EventArgs e)
        {
            frmAccountManagement accountForm = new frmAccountManagement();
            accountForm.ShowDialog();
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePasswordForm = new frmChangePassword();
            changePasswordForm.ShowDialog();
        }

        private void menuSalesReport_Click(object sender, EventArgs e)
        {
            // SỬA LỖI: Sử dụng tên lớp Form 'frmSalesReport'
            frmSalesReport reportForm = new frmSalesReport();
            reportForm.ShowDialog();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            frmAbout aboutForm = new frmAbout();
            aboutForm.ShowDialog();
        }
        private void menuOrderHistory_Click(object sender, EventArgs e)
        {
            // 1. Tạo một đối tượng mới của form bạn muốn mở
            frmOrderHistory historyForm = new frmOrderHistory();

            // 2. Hiển thị form đó dưới dạng một hộp thoại
            //    (ngăn người dùng tương tác với frmMain cho đến khi form này đóng lại)
            historyForm.ShowDialog();
        }
    }
}