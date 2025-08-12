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
    public partial class frmCustomerManagement : Form
    {
        private readonly CustomerBLL _customerBLL = new CustomerBLL();

        public frmCustomerManagement()
        {
            InitializeComponent();
        }

        private void frmCustomerManagement_Load(object sender, EventArgs e)
        {
            LoadCustomersGrid();
            SetControlsState(false);
            //ApplyRolePermissions();
        }

        private void LoadCustomersGrid()
        {
            dgvCustomers.DataSource = _customerBLL.GetAllCustomers();
        }
        private void ApplyRolePermissions()
        {
            bool canEdit = (UserSession.RoleName == "Admin");

            btnNew.Enabled = canEdit;
            btnSave.Enabled = canEdit;
            btnDelete.Enabled = canEdit;

            if (!canEdit)
            {
                txtFullName.Enabled = false;
                txtPhone.Enabled = false;
                txtAddress.Enabled = false;
            }
        }

        private void SetControlsState(bool isEditing)
        {
            // Bước 1: Kiểm tra quyền của người dùng trước tiên
            bool canEdit = (UserSession.RoleName == "Admin");

            // Bước 2: Bật/tắt các ô nhập liệu
            // Chỉ cho phép nhập liệu khi đang ở chế độ chỉnh sửa VÀ có quyền
            txtFullName.Enabled = isEditing && canEdit;
            txtPhone.Enabled = isEditing && canEdit;
            txtAddress.Enabled = isEditing && canEdit;

            // Bước 3: Bật/tắt các nút bấm
            // Nút "Save" chỉ bật khi đang chỉnh sửa VÀ có quyền
            btnSave.Enabled = isEditing && canEdit;

            // Nút "New" chỉ bật khi KHÔNG đang chỉnh sửa VÀ có quyền
            btnNew.Enabled = !isEditing && canEdit;

            // Nút "Delete" chỉ bật khi KHÔNG đang chỉnh sửa, có dòng được chọn, VÀ có quyền
            btnDelete.Enabled = !isEditing && (dgvCustomers.SelectedRows.Count > 0) && canEdit;
        }

        private void ClearForm()
        {
            txtCustomerId.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            dgvCustomers.ClearSelection();
            SetControlsState(false);
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvCustomers.SelectedRows[0];
                txtCustomerId.Text = selectedRow.Cells["CustomerID"].Value.ToString();
                txtFullName.Text = selectedRow.Cells["FullName"].Value.ToString();
                txtPhone.Text = selectedRow.Cells["Phone"].Value?.ToString();
                txtAddress.Text = selectedRow.Cells["Address"].Value?.ToString();
                SetControlsState(false);
            }
            else
            {
                SetControlsState(false);
                btnDelete.Enabled = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetControlsState(true);
            txtFullName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CustomerDTO customer = new CustomerDTO
            {
                CustomerID = string.IsNullOrEmpty(txtCustomerId.Text) ? 0 : int.Parse(txtCustomerId.Text),
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };

            string result = _customerBLL.SaveCustomer(customer);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Customer saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCustomersGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                int customerId = int.Parse(txtCustomerId.Text);
                bool success = _customerBLL.DeleteCustomer(customerId);
                if (success)
                {
                    MessageBox.Show("Customer deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomersGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to delete the customer. They may be linked to existing orders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
