using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DAL;

namespace StoreManagementSystem_Trung.BLL
{
    public class DashboardBLL
    {
        private readonly DashboardDAL _dashboardDAL = new DashboardDAL();

        public int GetProductCount()
        {
            return _dashboardDAL.GetProductCount();
        }

        public int GetCustomerCount()
        {
            return _dashboardDAL.GetCustomerCount();
        }

        public decimal GetTotalRevenueThisMonth()
        {
            return _dashboardDAL.GetTotalRevenueThisMonth();
        }

        public DataTable GetTopSellingProducts()
        {
            return _dashboardDAL.GetTopSellingProducts();
        }
        public DataTable GetSalesReport(DateTime startDate, DateTime endDate)
        {
            // Basic validation
            if (startDate > endDate)
            {
                return null; // Return null if the date range is invalid
            }
            return _dashboardDAL.GetSalesReport(startDate, endDate);
        }
    }
}
