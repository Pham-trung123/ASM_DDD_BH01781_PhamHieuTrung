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
    public partial class frmProductManagement : Form
    {
        private readonly ProductBLL _productBLL = new ProductBLL();
        private readonly CategoryBLL _categoryBLL = new CategoryBLL();

        public frmProductManagement()
        {
            InitializeComponent();
        }

        private void frmProductManagement_Load(object sender, EventArgs e)
        {
            LoadProductsGrid();
            LoadCategoriesComboBox();
            SetControlsState(false);
        }

        private void LoadProductsGrid()
        {
            dgvProducts.DataSource = _productBLL.GetAllProducts();
        }

        private void LoadCategoriesComboBox()
        {
            cmbCategory.DataSource = _categoryBLL.GetAllCategories();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            cmbCategory.SelectedIndex = -1; // No category selected initially
        }

        private void SetControlsState(bool isEditing)
        {
            bool canEdit = (UserSession.RoleName == "Admin" || UserSession.RoleName == "Warehouse");

            // Bước 2: Bật/tắt các ô nhập liệu
            txtProductName.Enabled = isEditing && canEdit;
            numPrice.Enabled = isEditing && canEdit;
            numQuantity.Enabled = isEditing && canEdit;
            cmbCategory.Enabled = isEditing && canEdit;

            // Bước 3: Bật/tắt các nút bấm
            btnSave.Enabled = isEditing && canEdit;
            btnNew.Enabled = !isEditing && canEdit;
            btnDelete.Enabled = !isEditing && (dgvProducts.SelectedRows.Count > 0) && canEdit;
        }

        private void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            numPrice.Value = 0;
            numQuantity.Value = 0;
            cmbCategory.SelectedIndex = -1;
            dgvProducts.ClearSelection();
            SetControlsState(false);
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];
                txtProductId.Text = selectedRow.Cells["ProductID"].Value.ToString();
                txtProductName.Text = selectedRow.Cells["ProductName"].Value.ToString();
                numPrice.Value = Convert.ToDecimal(selectedRow.Cells["UnitPrice"].Value);
                numQuantity.Value = Convert.ToInt32(selectedRow.Cells["Quantity"].Value);

                // Find and select the category in the ComboBox
                string categoryName = selectedRow.Cells["CategoryName"].Value.ToString();
                if (!string.IsNullOrEmpty(categoryName))
                {
                    cmbCategory.SelectedIndex = cmbCategory.FindStringExact(categoryName);
                }
                else
                {
                    cmbCategory.SelectedIndex = -1;
                }
                SetControlsState(false); // Allow editing
            }
            else
            {
                SetControlsState(false);
                //btnDelete.Enabled = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetControlsState(true);
            txtProductName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProductDTO product = new ProductDTO
            {
                ProductID = string.IsNullOrEmpty(txtProductId.Text) ? 0 : int.Parse(txtProductId.Text),
                ProductName = txtProductName.Text.Trim(),
                UnitPrice = numPrice.Value,
                Quantity = (int)numQuantity.Value,
                CategoryID = (int?)cmbCategory.SelectedValue
            };

            string result = _productBLL.SaveProduct(product);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Product saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProductsGrid();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                int productId = int.Parse(txtProductId.Text);
                bool success = _productBLL.DeleteProduct(productId);
                if (success)
                {
                    MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductsGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Failed to delete the product. It might be used in an order.", "Error", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Lấy DataTable đang được bind với DataGridView
            DataTable dt = (DataTable)dgvProducts.DataSource;
            if (dt != null)
            {
                // Áp dụng bộ lọc cho cột ProductName
                // '%{txtSearch.Text}%' nghĩa là tìm kiếm bất cứ đâu trong chuỗi
                dt.DefaultView.RowFilter = $"ProductName LIKE '%{txtSearch.Text}%'";
            }
        }

    }
}
