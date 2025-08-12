using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystem_Trung.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        public decimal TotalAmount { get; set; }

        // Property này không có trong CSDL, dùng để chứa danh sách chi tiết đơn hàng
        // khi cần lấy thông tin đầy đủ của một đơn hàng.
        public List<OrderDetailDTO> OrderDetails { get; set; }

        public OrderDTO()
        {
            OrderDetails = new List<OrderDetailDTO>();
        }
    }
}
