using GIBS.Module.RestaurantMenu.Models;
using Oqtane.Enums;
using Oqtane.Services;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuAttributeService : ServiceBase, IMenuAttributeService
    {
        public MenuAttributeService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("MenuAttribute");

        public async Task<List<MenuAttribute>> GetMenuAttributesAsync(int moduleId)
        {
            List<MenuAttribute> menuAttributes = await GetJsonAsync<List<MenuAttribute>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={moduleId}", EntityNames.Module, moduleId), Enumerable.Empty<MenuAttribute>().ToList());
            return menuAttributes.OrderBy(item => item.SortOrder).ThenBy(item => item.AttributeName).ToList();
        }

        public async Task<MenuAttribute> GetMenuAttributeAsync(int attributeId, int moduleId)
        {
            return await GetJsonAsync<MenuAttribute>(CreateAuthorizationPolicyUrl($"{Apiurl}/{attributeId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }

        public async Task<MenuAttribute> AddMenuAttributeAsync(MenuAttribute attribute)
        {
            return await PostJsonAsync<MenuAttribute>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={attribute.ModuleId}", EntityNames.Module, attribute.ModuleId), attribute);
        }

        public async Task<MenuAttribute> UpdateMenuAttributeAsync(MenuAttribute attribute)
        {
            return await PutJsonAsync<MenuAttribute>(CreateAuthorizationPolicyUrl($"{Apiurl}/{attribute.AttributeId}?moduleid={attribute.ModuleId}", EntityNames.Module, attribute.ModuleId), attribute);
        }

        public async Task DeleteMenuAttributeAsync(int attributeId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{attributeId}?moduleid={moduleId}", EntityNames.Module, moduleId));
        }
    }
}