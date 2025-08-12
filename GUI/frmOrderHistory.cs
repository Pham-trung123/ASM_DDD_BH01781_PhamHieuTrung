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

namespace StoreManagementSystem_Trung.GUI
{
    public partial class frmOrderHistory : Form
    {
        // Tạo một instance của OrderBLL để sử dụng trong form
        private readonly OrderBLL _orderBLL = new OrderBLL();

        public frmOrderHistory()
        {
            // Hàm này rất quan trọng để khởi tạo các controls từ file Designer
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện này được kích hoạt khi form được tải lên lần đầu tiên.
        /// Đây là nơi tốt nhất để tải dữ liệu ban đầu.
        /// </summary>
        private void frmOrderHistory_Load(object sender, EventArgs e)
        {
            LoadOrdersGrid();
        }

        /// <summary>
        /// Tải danh sách tất cả các đơn hàng vào DataGridView bên trái.
        /// </summary>
        private void LoadOrdersGrid()
        {
            // Gọi BLL để lấy dữ liệu từ CSDL
            dgvOrders.DataSource = _orderBLL.GetAllOrders();

            // Tùy chỉnh tiêu đề cột để dễ đọc hơn
            dgvOrders.Columns["OrderID"].HeaderText = "Order ID";
            dgvOrders.Columns["OrderDate"].HeaderText = "Date";
            dgvOrders.Columns["CustomerName"].HeaderText = "Customer";
            dgvOrders.Columns["EmployeeName"].HeaderText = "Sold By";
            dgvOrders.Columns["TotalAmount"].HeaderText = "Total";
            // Định dạng cột tiền tệ
            dgvOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
        }

        /// <summary>
        /// Sự kiện này được kích hoạt mỗi khi người dùng chọn một dòng khác trong danh sách đơn hàng.
        /// </summary>
        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào đang được chọn không
            if (dgvOrders.SelectedRows.Count > 0)
            {
                // Lấy OrderID từ ô "OrderID" của dòng được chọn
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderID"].Value);

                // Cập nhật lại nhãn thông tin
                lblOrderInfo.Text = $"Details for Order ID: {orderId}";

                // Tải chi tiết của đơn hàng tương ứng vào DataGridView bên phải
                LoadOrderDetails(orderId);
            }
            else
            {
                // Nếu không có dòng nào được chọn, xóa sạch lưới chi tiết và reset nhãn
                dgvOrderDetails.DataSource = null;
                lblOrderInfo.Text = "Select an order to view details";
            }
        }

        /// <summary>
        /// Tải chi tiết của một đơn hàng cụ thể dựa vào orderId.
        /// </summary>
        private void LoadOrderDetails(int orderId)
        {
            // Gọi BLL để lấy dữ liệu chi tiết
            DataTable details = _orderBLL.GetOrderDetailsByOrderId(orderId);
            dgvOrderDetails.DataSource = details;

            // Nếu có dữ liệu, tùy chỉnh lại tiêu đề cột
            if (details != null)
            {
                dgvOrderDetails.Columns["ProductName"].HeaderText = "Product";
                dgvOrderDetails.Columns["Quantity"].HeaderText = "Qty";
                dgvOrderDetails.Columns["UnitPrice"].HeaderText = "Price";
                // Định dạng cột tiền tệ
                dgvOrderDetails.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
            }
        }

    }
}
