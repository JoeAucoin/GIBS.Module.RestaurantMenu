using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Services
{
    public interface IMenuItemService
    {

        Task<List<MenuItem>> GetMenuItemsAsync(int moduleId);
        Task<MenuItem> GetMenuItemAsync(int menuItemId, int moduleId);
        Task<MenuItem> AddMenuItemAsync(MenuItem menuItem);
        Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem);
        Task DeleteMenuItemAsync(int menuItemId, int moduleId);

        // Add the missing moduleId parameter to match the implementation
        Task UpdateMenuItemAttributesAsync(int menuItemId, int moduleId, List<int> attributeIds);

        Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId, int moduleId);

        // Add this method to support attribute retrieval for a menu item
        Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId);

        Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId);

        Task<int> GetMaxSortOrderByCategoryAsync(int categoryId, int moduleId);

        //Task<List<Models.MenuItem>> GetMenuItemsAsync(int moduleId);
        //Task<Models.MenuItem> GetMenuItemAsync(int menuItemId, int moduleId);
        //Task<Models.MenuItem> AddMenuItemAsync(Models.MenuItem menuItem);
        //Task<Models.MenuItem> UpdateMenuItemAsync(Models.MenuItem menuItem);
        //Task DeleteMenuItemAsync(int menuItemId, int moduleId);

        //Task UpdateMenuItemAttributesAsync(int menuItemId, List<int> attributeIds);


        //// Add this new method for getting items by category
        //Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId, int moduleId);
    }
}