using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public interface IMenuItemRepository
    {
        IEnumerable<MenuItem> GetMenuItems(int moduleId);
        Task<MenuItem> GetMenuItemAsync(int menuItemId, int moduleId);
        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
        Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int menuItemId, int moduleId);

        Task UpdateMenuItemAttributesAsync(int menuItemId, int moduleId, List<int> attributeIds);
        Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId);

        // Add this new method
        IEnumerable<MenuItem> GetMenuItemsByCategory(int categoryId);

        // Add this:
        Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId);

        Task<int> GetMaxSortOrderByCategoryAsync(int categoryId, int moduleId);
    }
}