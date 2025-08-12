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
    public partial class frmSalesReport : Form
    {
        private readonly DashboardBLL _dashboardBLL = new DashboardBLL();

        public frmSalesReport()
        {
            InitializeComponent();
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            // Set default dates: from the first day of the current month to today
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be after the end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable reportData = _dashboardBLL.GetSalesReport(startDate, endDate);

            if (reportData == null)
            {
                MessageBox.Show("Could not generate the report.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvReport.DataSource = reportData;
            CustomizeGrid();
            CalculateTotalRevenue(reportData);
        }

        private void CustomizeGrid()
        {
            if (dgvReport.Columns.Count > 0)
            {
                dgvReport.Columns["OrderID"].HeaderText = "Order ID";
                dgvReport.Columns["OrderDate"].HeaderText = "Date";
                dgvReport.Columns["CustomerName"].HeaderText = "Customer";
                dgvReport.Columns["EmployeeName"].HeaderText = "Sold By";
                dgvReport.Columns["TotalAmount"].HeaderText = "Total";
                dgvReport.Columns["TotalAmount"].DefaultCellStyle.Format = "C2"; // Currency format
            }
        }

        private void CalculateTotalRevenue(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                lblTotalRevenueValue.Text = (0).ToString("C2");
                return;
            }

            // Calculate the sum of the "TotalAmount" column
            decimal totalRevenue = dataTable.AsEnumerable()
                                            .Sum(row => row.Field<decimal>("TotalAmount"));

            lblTotalRevenueValue.Text = totalRevenue.ToString("C2");
        }

    }
}
