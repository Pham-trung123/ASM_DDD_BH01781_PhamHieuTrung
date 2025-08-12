using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagementSystem_Trung.DAL;
using StoreManagementSystem_Trung.DTO;

namespace StoreManagementSystem_Trung.BLL
{
    public class CustomerBLL
    {
        private readonly CustomerDAL _customerDAL = new CustomerDAL();

        public DataTable GetAllCustomers()
        {
            return _customerDAL.GetAllCustomers();
        }

        public string SaveCustomer(CustomerDTO customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FullName))
                return "Customer name cannot be empty.";

            bool result = (customer.CustomerID == 0)
                ? _customerDAL.AddCustomer(customer)
                : _customerDAL.UpdateCustomer(customer);

            return result ? "" : "Failed to save the customer.";
        }

        public bool DeleteCustomer(int customerId)
        {
            return _customerDAL.DeleteCustomer(customerId);
        }
    }
}
