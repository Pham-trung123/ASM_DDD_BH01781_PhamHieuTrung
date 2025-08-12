using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DTO;
using StoreManagementSystem_Trung.DAL;
using System.Data;

namespace StoreManagementSystem_Trung.BLL
{
    public class OrderBLL
    {
        private readonly OrderDAL _orderDAL;

        public OrderBLL()
        {
            _orderDAL = new OrderDAL();
        }

        public string CreateOrder(OrderDTO order)
        {
            // --- Validation ---
            if (order.CustomerID == 0)
            {
                return "Please select a customer.";
            }
            if (order.EmployeeID == 0)
            {
                return "Could not identify the sales employee.";
            }
            if (order.OrderDetails.Count == 0)
            {
                return "An order must contain at least one product.";
            }

            order.OrderDate = DateTime.Now;

            decimal totalAmount = 0;
            foreach (var detail in order.OrderDetails)
            {
                totalAmount += detail.Quantity * detail.Price;
            }
            order.TotalAmount = totalAmount;

            bool result = _orderDAL.CreateOrder(order);

            return result ? "" : "Failed to create the order in the database. Please try again.";
        }
        public DataTable GetAllOrders()
        {
            return _orderDAL.GetAllOrders();
        }

        public DataTable GetOrderDetailsByOrderId(int orderId)
        {
            if (orderId <= 0)
            {
                return null;
            }
            return _orderDAL.GetOrderDetailsByOrderId(orderId);
        }
    }
}
