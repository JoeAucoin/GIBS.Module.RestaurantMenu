using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class RestaurantMenuService : ServiceBase, IRestaurantMenuService
    {
        public RestaurantMenuService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("RestaurantMenu");

        public async Task<List<Models.RestaurantMenu>> GetRestaurantMenusAsync(int ModuleId)
        {
            List<Models.RestaurantMenu> RestaurantMenus = await GetJsonAsync<List<Models.RestaurantMenu>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.RestaurantMenu>().ToList());
            return RestaurantMenus.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.RestaurantMenu> GetRestaurantMenuAsync(int RestaurantMenuId, int ModuleId)
        {
            return await GetJsonAsync<Models.RestaurantMenu>(CreateAuthorizationPolicyUrl($"{Apiurl}/{RestaurantMenuId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.RestaurantMenu> AddRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu)
        {
            return await PostJsonAsync<Models.RestaurantMenu>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, RestaurantMenu.ModuleId), RestaurantMenu);
        }

        public async Task<Models.RestaurantMenu> UpdateRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu)
        {
            return await PutJsonAsync<Models.RestaurantMenu>(CreateAuthorizationPolicyUrl($"{Apiurl}/{RestaurantMenu.RestaurantMenuId}", EntityNames.Module, RestaurantMenu.ModuleId), RestaurantMenu);
        }

        public async Task DeleteRestaurantMenuAsync(int RestaurantMenuId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{RestaurantMenuId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
