using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models; // Ensure Models namespace is available for MenuCategory

namespace GIBS.Module.RestaurantMenu.Repository
{

    public interface IMenuCategoryRepository
    {
        // Task<List<Models.RestaurantMenu>> GetMenuCategoriesAsync(int moduleId);

        IEnumerable<MenuCategory> GetMenuCategories(int ModuleId); // Changed RestaurantMenu to MenuCategory

        Task<MenuCategory> GetMenuCategoryAsync(int categoryId, int moduleId); // Assuming this was added
        Task<MenuCategory> AddMenuCategoryAsync(MenuCategory menuCategory); // Assuming this was added
        Task<MenuCategory> UpdateMenuCategoryAsync(MenuCategory menuCategory); // Assuming this was added
        Task DeleteMenuCategoryAsync(int categoryId, int moduleId); // Add this line

        Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId);

    }
}
