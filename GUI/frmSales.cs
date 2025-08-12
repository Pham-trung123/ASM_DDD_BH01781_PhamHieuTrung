using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using StoreManagementSystem_Trung.BLL;
using StoreManagementSystem_Trung.DAL;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung
{
    public partial class frmSales : Form
    {
        private readonly CustomerBLL _customerBLL = new CustomerBLL();
        private readonly ProductBLL _productBLL = new ProductBLL();
        private readonly OrderBLL _orderBLL = new OrderBLL();

        private BindingList<CartItem> _cart = new BindingList<CartItem>();

        public frmSales()
        {
            // Gọi hàm này để khởi tạo các controls từ file Designer
            InitializeComponent();
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadProducts();
            SetupCartGrid();
        }

        private void LoadCustomers()
        {
            cmbCustomers.DataSource = _customerBLL.GetAllCustomers();
            cmbCustomers.DisplayMember = "FullName";
            cmbCustomers.ValueMember = "CustomerID";
            cmbCustomers.SelectedIndex = -1;
        }

        private void LoadProducts()
        {
            cmbProducts.DataSource = _productBLL.GetAllProducts();
            cmbProducts.DisplayMember = "ProductName";
            cmbProducts.ValueMember = "ProductID";
            cmbProducts.SelectedIndex = -1;
        }

        private void SetupCartGrid()
        {
            dgvCart.DataSource = _cart;
            dgvCart.Columns["ProductID"].Visible = false;
            dgvCart.Columns["ProductName"].HeaderText = "Product";
            dgvCart.Columns["Quantity"].HeaderText = "Qty";
            dgvCart.Columns["UnitPrice"].HeaderText = "Price";
            dgvCart.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvCart.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            dgvCart.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedValue == null)
            {
                MessageBox.Show("Please select a product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = (int)cmbProducts.SelectedValue;
            DataRowView selectedProductRow = (DataRowView)cmbProducts.SelectedItem;
            string productName = selectedProductRow["ProductName"].ToString();
            decimal unitPrice = Convert.ToDecimal(selectedProductRow["UnitPrice"]);
            int quantityToAdd = (int)numQuantity.Value;

            CartItem existingItem = _cart.FirstOrDefault(item => item.ProductID == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantityToAdd;
            }
            else
            {
                _cart.Add(new CartItem
                {
                    ProductID = productId,
                    ProductName = productName,
                    Quantity = quantityToAdd,
                    UnitPrice = unitPrice
                });
            }

            _cart.ResetBindings();
            UpdateTotalAmount();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                CartItem selectedItem = (CartItem)dgvCart.SelectedRows[0].DataBoundItem;
                _cart.Remove(selectedItem);
                UpdateTotalAmount();
            }
        }

        private void UpdateTotalAmount()
        {
            decimal total = _cart.Sum(item => item.Subtotal);
            lblTotalAmount.Text = total.ToString("N2");
        }

        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if (cmbCustomers.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_cart.Count == 0)
            {
                MessageBox.Show("The cart is empty. Please add at least one product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OrderDTO newOrder = new OrderDTO
            {
                CustomerID = (int)cmbCustomers.SelectedValue,
                EmployeeID = GetCurrentEmployeeId(),
                OrderDate = DateTime.Now,
                TotalAmount = _cart.Sum(item => item.Subtotal)
            };

            foreach (var cartItem in _cart)
            {
                newOrder.OrderDetails.Add(new OrderDetailDTO
                {
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.UnitPrice
                });
            }

            string result = _orderBLL.CreateOrder(newOrder);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Order created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cart.Clear();
                cmbCustomers.SelectedIndex = -1;
                cmbProducts.SelectedIndex = -1;
                numQuantity.Value = 1;
                UpdateTotalAmount();
            }
            else
            {
                MessageBox.Show(result, "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCurrentEmployeeId()
        {
            return UserSession.EmployeeID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public class CartItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Subtotal => Quantity * UnitPrice;
        }
    }
}