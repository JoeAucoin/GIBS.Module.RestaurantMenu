using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Services
{
    public interface IMenuOptionService
    {
        Task<List<MenuOption>> GetMenuOptionsByItemIdAsync(int itemId);
        Task<MenuOption> GetMenuOptionAsync(int optionId);
        Task<MenuOption> AddMenuOptionAsync(MenuOption option);
        Task<MenuOption> UpdateMenuOptionAsync(MenuOption option);
        Task DeleteMenuOptionAsync(int optionId);
    }
}