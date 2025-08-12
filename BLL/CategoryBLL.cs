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
    public class CategoryBLL
    {
        private readonly CategoryDAL _categoryDAL = new CategoryDAL();
        public DataTable GetAllCategories()
        {
            return _categoryDAL.GetAllCategories();
        }
        public string SaveCategory(CategoryDTO category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return "Category name cannot be empty.";
            }

            // In a real app, you should check for duplicate category names here before saving.

            bool result;
            if (category.CategoryID == 0) // New category
            {
                result = _categoryDAL.AddCategory(category.CategoryName);
            }
            else // Update existing category
            {
                result = _categoryDAL.UpdateCategory(category);
            }

            return result ? "" : "Failed to save the category.";
        }

        public bool DeleteCategory(int categoryId)
        {
            return _categoryDAL.DeleteCategory(categoryId);
        }

    }
}
