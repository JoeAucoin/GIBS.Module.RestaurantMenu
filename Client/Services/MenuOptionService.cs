using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Services;
using Oqtane.Enums;
using Oqtane.Services;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Client.Services
{
    public class MenuOptionService : ServiceBase, IMenuOptionService
    {
        public MenuOptionService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("MenuOption");

        public async Task<List<MenuOption>> GetMenuOptionsByItemIdAsync(int itemId)
        {
            var url = $"{Apiurl}/byitem/{itemId}";
            List<MenuOption> options = await GetJsonAsync<List<MenuOption>>(url, new List<MenuOption>());
            return options.OrderBy(o => o.OptionName).ToList();
        }

        public async Task<MenuOption> GetMenuOptionAsync(int optionId)
        {
            var url = $"{Apiurl}/{optionId}";
            return await GetJsonAsync<MenuOption>(url);
        }

        public async Task<MenuOption> AddMenuOptionAsync(MenuOption option)
        {
            var url = $"{Apiurl}";
            return await PostJsonAsync<MenuOption>(url, option);
        }

        public async Task<MenuOption> UpdateMenuOptionAsync(MenuOption option)
        {
            var url = $"{Apiurl}/{option.OptionId}";
            return await PutJsonAsync<MenuOption>(url, option);
        }

        public async Task DeleteMenuOptionAsync(int optionId)
        {
            var url = $"{Apiurl}/{optionId}";
            await DeleteAsync(url);
        }
    }
}