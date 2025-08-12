using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using StoreManagementSystem_Trung.BLL;

namespace StoreManagementSystem_Trung.GUI
{
    public partial class frmDashboard : Form
    {
        private readonly DashboardBLL _dashboardBLL = new DashboardBLL();

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Load summary panels
            lblProductCount.Text = _dashboardBLL.GetProductCount().ToString();
            lblCustomerCount.Text = _dashboardBLL.GetCustomerCount().ToString();
            lblRevenueAmount.Text = _dashboardBLL.GetTotalRevenueThisMonth().ToString("C"); // "C" formats as currency

            // Load chart data
            LoadTopProductsChart();
        }

        private void LoadTopProductsChart()
        {
            DataTable dt = _dashboardBLL.GetTopSellingProducts();

            // Clear previous data
            chartTopProducts.Series.Clear();
            chartTopProducts.Titles.Clear();

            // Add a title
            chartTopProducts.Titles.Add("Top 5 Selling Products");

            // Add a new series for the bar chart
            Series series = new Series("TopProducts")
            {
                ChartType = SeriesChartType.Bar // Use a bar chart
            };

            // Bind data to the chart
            // XValueMember: What to show on the Y-axis labels (product names)
            // YValueMembers: What determines the bar length (quantity sold)
            series.XValueMember = "ProductName";
            series.YValueMembers = "TotalQuantitySold";
            chartTopProducts.DataSource = dt;
            chartTopProducts.Series.Add(series);

            // Customize chart appearance
            chartTopProducts.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTopProducts.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartTopProducts.Legends[0].Enabled = false; // Hide the legend
        }
    }
}
