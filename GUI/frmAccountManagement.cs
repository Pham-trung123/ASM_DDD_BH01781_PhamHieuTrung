using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreManagementSystem_Trung.BLL;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.GUI
{
    // Quan trọng nhất: Lớp phải kế thừa từ Form
    public partial class frmAccountManagement : Form
    {
        private readonly EmployeeBLL _employeeBLL = new EmployeeBLL();
        private readonly RoleBLL _roleBLL = new RoleBLL();
        private readonly AccountBLL _accountBLL = new AccountBLL();

        public frmAccountManagement()
        {
            // Quan trọng nhất: Phải gọi hàm này để tạo các controls
            InitializeComponent();
        }

        private void frmAccountManagement_Load(object sender, EventArgs e)
        {
            LoadEmployeesWithoutAccount();
            LoadRoles();
        }

        private void LoadEmployeesWithoutAccount()
        {
            cmbEmployees.DataSource = _employeeBLL.GetEmployeesWithoutAccount();
            cmbEmployees.DisplayMember = "FullName";
            cmbEmployees.ValueMember = "EmployeeID";
            cmbEmployees.SelectedIndex = -1;
        }

        private void LoadRoles()
        {
            cmbRoles.DataSource = _roleBLL.GetAllRoles();
            cmbRoles.DisplayMember = "RoleName";
            cmbRoles.ValueMember = "RoleID";
            cmbRoles.SelectedIndex = -1;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbEmployees.SelectedValue == null || cmbRoles.SelectedValue == null)
            {
                MessageBox.Show("Please select both an employee and a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AccountDTO newAccount = new AccountDTO
            {
                Username = txtUsername.Text.Trim(),
                EmployeeID = (int)cmbEmployees.SelectedValue,
                RoleID = (int)cmbRoles.SelectedValue
            };

            string result = _accountBLL.CreateAccount(newAccount);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Account created successfully! Default password is '12з456'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Tải lại danh sách nhân viên chưa có tài khoản
                LoadEmployeesWithoutAccount();
                txtUsername.Clear();
                cmbRoles.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}