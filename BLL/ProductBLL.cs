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
    public class ProductBLL
    {
        private readonly ProductDAL _productDAL = new ProductDAL();

        public DataTable GetAllProducts()
        {
            return _productDAL.GetAllProducts();
        }

        public string SaveProduct(ProductDTO product)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(product.ProductName))
                return "Product name cannot be empty.";
            if (product.UnitPrice < 0)
                return "Unit price cannot be negative.";
            if (product.Quantity < 0)
                return "Quantity cannot be negative.";

            bool result;
            if (product.ProductID == 0) // New product
            {
                result = _productDAL.AddProduct(product);
            }
            else // Existing product
            {
                result = _productDAL.UpdateProduct(product);
            }

            return result ? "" : "Failed to save the product.";
        }

        public bool DeleteProduct(int productId)
        {
            return _productDAL.DeleteProduct(productId);
        }
    }
}
