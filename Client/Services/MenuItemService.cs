using GIBS.Module.RestaurantMenu.Models;
using Oqtane.Enums;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuItemService : ServiceBase, IMenuItemService, IService
    {
        public MenuItemService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("MenuItem");

        public async Task<List<MenuItem>> GetMenuItemsAsync(int moduleId)
        {
            List<MenuItem> menuItems = await GetJsonAsync<List<MenuItem>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={moduleId}", EntityNames.Module, moduleId), Enumerable.Empty<MenuItem>().ToList());
            return menuItems.OrderBy(item => item.SortOrder).ThenBy(item => item.ItemName).ToList();
        }

        public async Task<MenuItem> GetMenuItemAsync(int menuItemId, int moduleId)
        {
            return await GetJsonAsync<MenuItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{menuItemId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            return await PostJsonAsync<MenuItem>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={menuItem.ModuleId}", EntityNames.Module, menuItem.ModuleId), menuItem);
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            await PutJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{menuItem.ItemId}?moduleid={menuItem.ModuleId}", EntityNames.Module, menuItem.ModuleId), menuItem);
            return menuItem;
        }

        public async Task DeleteMenuItemAsync(int menuItemId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{menuItemId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId, int moduleId)
        {
            List<MenuItem> menuItems = await GetJsonAsync<List<MenuItem>>(CreateAuthorizationPolicyUrl($"{Apiurl}/category/{categoryId}?moduleid={moduleId}", EntityNames.Module, moduleId), Enumerable.Empty<MenuItem>().ToList());
            return menuItems.OrderBy(item => item.SortOrder).ThenBy(item => item.ItemName).ToList();
        }

        public async Task UpdateMenuItemAttributesAsync(int menuItemId, int moduleId, List<int> attributeIds)
        {
            var url = CreateAuthorizationPolicyUrl($"{Apiurl}/{menuItemId}/attributes?moduleid={moduleId}", EntityNames.Module, moduleId);
            await PutJsonAsync(url, attributeIds);
        }

        public async Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId)
        {
            var url = CreateAuthorizationPolicyUrl($"{Apiurl}/{menuItemId}/attributes?moduleid={moduleId}", EntityNames.Module, moduleId);
            return await GetJsonAsync<List<MenuItemAttribute>>(url, new List<MenuItemAttribute>());
        }

        public async Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId)
        {
            var url = CreateAuthorizationPolicyUrl($"{Apiurl}/withattributes/{moduleId}", EntityNames.Module, moduleId);
            return await GetJsonAsync<List<MenuItem>>(url, new List<MenuItem>());
        }
        



        // Uncommented method for clarity
        // public async Task<int> GetMaxSortOrderByCategoryIdAsync(int categoryId, int moduleId)
        // {
        //     var url = CreateAuthorizationPolicyUrl($"{Apiurl}/maxsortorder/{categoryId}?moduleid={moduleId}", EntityNames.Module, moduleId);
        //     return await GetJsonAsync<int>(url, 0);
        // }
    }
}