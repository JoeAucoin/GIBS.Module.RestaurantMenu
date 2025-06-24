using GIBS.Module.RestaurantMenu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Services
{
    public interface IMenuCategoryService
    {

        Task<List<Models.MenuCategory>> GetMenuCategoriesAsync(int moduleId); // Added method to get all menu categories for a module

        Task<Models.MenuCategory> GetMenuCategoryAsync(int categoryId, int moduleId); // Assuming this was added for SectionsEdit
        Task<Models.MenuCategory> AddMenuCategoryAsync(Models.MenuCategory menuCategory); // Assuming this was added for SectionsEdit
        Task<Models.MenuCategory> UpdateMenuCategoryAsync(Models.MenuCategory menuCategory); // Assuming this was added for SectionsEdit
        Task DeleteMenuCategoryAsync(int categoryId, int moduleId); // Add this line

        Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId);
    }
}
