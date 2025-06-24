using GIBS.Module.RestaurantMenu.Models;
using Oqtane.Services;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuCategoryService : ServiceBase, IMenuCategoryService
    {
        public MenuCategoryService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("MenuCategory");

        public async Task<List<Models.MenuCategory>> GetMenuCategoriesAsync(int ModuleId)
        {
            List<Models.MenuCategory> menuCategories = await GetJsonAsync<List<Models.MenuCategory>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.MenuCategory>().ToList());
            return menuCategories;
        }

        public async Task<MenuCategory> GetMenuCategoryAsync(int categoryId, int moduleId)
        {
            return await GetJsonAsync<MenuCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}/{categoryId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }

        public async Task<MenuCategory> AddMenuCategoryAsync(MenuCategory menuCategory)
        {
            return await PostJsonAsync<MenuCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={menuCategory.ModuleId}", EntityNames.Module, menuCategory.ModuleId), menuCategory);
        }

        public async Task<MenuCategory> UpdateMenuCategoryAsync(MenuCategory menuCategory)
        {
            return await PutJsonAsync<MenuCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}/{menuCategory.CategoryId}?moduleid={menuCategory.ModuleId}", EntityNames.Module, menuCategory.ModuleId), menuCategory);
        }

        public async Task DeleteMenuCategoryAsync(int categoryId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{categoryId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }

        public async Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId)
        {
            var url = CreateAuthorizationPolicyUrl($"{Apiurl}/withmenu/{moduleId}", EntityNames.Module, moduleId);
            return await GetJsonAsync<List<MenuCategory>>(url, new List<MenuCategory>());
        }

    }
}