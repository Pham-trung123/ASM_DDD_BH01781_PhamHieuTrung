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
    public partial class frmEmployeeManagement : Form
    {
        private readonly EmployeeBLL _employeeBLL = new EmployeeBLL();

        public frmEmployeeManagement()
        {
            InitializeComponent();
        }

        private void frmEmployeeManagement_Load(object sender, EventArgs e)
        {
            LoadEmployeesGrid();
            SetControlsState(false);
        }

        private void LoadEmployeesGrid()
        {
            dgvEmployees.DataSource = _employeeBLL.GetAllEmployees();
        }

        private void SetControlsState(bool isEditing)
        {
            // Enable or disable textboxes
            txtFullName.Enabled = isEditing;
            txtPosition.Enabled = isEditing;
            txtPhone.Enabled = isEditing;
            txtEmail.Enabled = isEditing;

            // Enable or disable buttons
            btnSave.Enabled = isEditing;
            btnNew.Enabled = !isEditing;
            btnDelete.Enabled =(dgvEmployees.SelectedRows.Count > 0);
        }

        private void ClearForm()
        {
            txtEmployeeId.Clear();
            txtFullName.Clear();
            txtPosition.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            dgvEmployees.ClearSelection();
            SetControlsState(false);
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvEmployees.SelectedRows[0];
                txtEmployeeId.Text = selectedRow.Cells["EmployeeID"].Value.ToString();
                txtFullName.Text = selectedRow.Cells["FullName"].Value.ToString();
                txtPosition.Text = selectedRow.Cells["Position"].Value?.ToString();
                txtPhone.Text = selectedRow.Cells["Phone"].Value?.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value?.ToString();
                SetControlsState(true); // Allow editing
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
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeID = string.IsNullOrEmpty(txtEmployeeId.Text) ? 0 : int.Parse(txtEmployeeId.Text),
                FullName = txtFullName.Text.Trim(),
                Position = txtPosition.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            string result = _employeeBLL.SaveEmployee(employee);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Employee saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeesGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // A simple check to prevent deleting the admin (EmployeeID = 1)
            if (txtEmployeeId.Text == "1")
            {
                MessageBox.Show("The system administrator cannot be deleted.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this employee?\nThis action may fail if the employee is linked to an account or orders.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                int employeeId = int.Parse(txtEmployeeId.Text);
                bool success = _employeeBLL.DeleteEmployee(employeeId);
                if (success)
                {
                    MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeesGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to delete the employee. They may have an active account or have processed orders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
