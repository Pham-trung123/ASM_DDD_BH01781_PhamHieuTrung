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
    public partial class frmCategoryManagement : Form
    {
        private readonly CategoryBLL _categoryBLL = new CategoryBLL();

        public frmCategoryManagement()
        {
            InitializeComponent();
        }

        private void frmCategoryManagement_Load(object sender, EventArgs e)
        {
            LoadCategoriesGrid();
            SetControlsState(false);
        }

        private void LoadCategoriesGrid()
        {
            dgvCategories.DataSource = _categoryBLL.GetAllCategories();
        }

        private void SetControlsState(bool isEditing)
        {
            txtCategoryName.Enabled = isEditing;
            btnSave.Enabled = isEditing;
            btnNew.Enabled = !isEditing;
            btnDelete.Enabled =(dgvCategories.SelectedRows.Count > 0);
        }

        private void ClearForm()
        {
            txtCategoryId.Clear();
            txtCategoryName.Clear();
            dgvCategories.ClearSelection();
            SetControlsState(false);
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvCategories.SelectedRows[0];
                txtCategoryId.Text = selectedRow.Cells["CategoryID"].Value.ToString();
                txtCategoryName.Text = selectedRow.Cells["CategoryName"].Value.ToString();
                SetControlsState(true);
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
            txtCategoryName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CategoryDTO category = new CategoryDTO
            {
                CategoryID = string.IsNullOrEmpty(txtCategoryId.Text) ? 0 : int.Parse(txtCategoryId.Text),
                CategoryName = txtCategoryName.Text.Trim()
            };

            string result = _categoryBLL.SaveCategory(category);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Category saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategoriesGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a category to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                int categoryId = int.Parse(txtCategoryId.Text);
                try
                {
                    bool success = _categoryBLL.DeleteCategory(categoryId);
                    if (success)
                    {
                        MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCategoriesGrid();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Catch exception if the category is in use (foreign key constraint)
                    MessageBox.Show("Cannot delete this category because it is being used by one or more products.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}