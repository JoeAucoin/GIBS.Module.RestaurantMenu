using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oqtane.Modules;
using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Repository;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuItemService : IMenuItemService, IService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;

        }

        public Task<List<MenuItem>> GetMenuItemsAsync(int moduleId)
        {
            return Task.FromResult(_menuItemRepository.GetMenuItems(moduleId).ToList());
        }

        public async Task<MenuItem> GetMenuItemAsync(int menuItemId, int moduleId)
        {
            return await _menuItemRepository.GetMenuItemAsync(menuItemId, moduleId);
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            return await _menuItemRepository.AddMenuItemAsync(menuItem);
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            return await _menuItemRepository.UpdateMenuItemAsync(menuItem);
        }

        public async Task DeleteMenuItemAsync(int menuItemId, int moduleId)
        {
            await _menuItemRepository.DeleteMenuItemAsync(menuItemId, moduleId);
        }

        public async Task UpdateMenuItemAttributesAsync(int menuItemId, int moduleId, List<int> attributeIds)
        {
            // Security is handled by the controller, so we just pass through to the repository.
            await _menuItemRepository.UpdateMenuItemAttributesAsync(menuItemId, moduleId, attributeIds);
        }

        // Add the missing method implementation
        public Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId, int moduleId)
        {
            // Security is handled by the controller.
            return Task.FromResult(_menuItemRepository.GetMenuItemsByCategory(categoryId).ToList());
        }

        public async Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId)
        {
            return await _menuItemRepository.GetMenuItemAttributesAsync(menuItemId, moduleId);
        }

        public async Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId)
        {
            return await _menuItemRepository.GetMenuItemsWithAttributesAsync(moduleId);
        }

        public async Task<int> GetMaxSortOrderByCategoryAsync(int categoryId, int moduleId)
        {
            return await _menuItemRepository.GetMaxSortOrderByCategoryAsync(categoryId, moduleId);
        }

    }
}