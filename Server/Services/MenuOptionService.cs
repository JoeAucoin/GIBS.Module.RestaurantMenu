using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuOptionService : IMenuOptionService
    {
        private readonly IMenuOptionRepository _repo;

        public MenuOptionService(IMenuOptionRepository repo)
        {
            _repo = repo;
        }

        public Task<List<MenuOption>> GetMenuOptionsByItemIdAsync(int itemId) => _repo.GetMenuOptionsByItemIdAsync(itemId);
        public Task<MenuOption> GetMenuOptionAsync(int optionId) => _repo.GetMenuOptionAsync(optionId);
        public Task<MenuOption> AddMenuOptionAsync(MenuOption option) => _repo.AddMenuOptionAsync(option);
        public Task<MenuOption> UpdateMenuOptionAsync(MenuOption option) => _repo.UpdateMenuOptionAsync(option);
        public Task DeleteMenuOptionAsync(int optionId) => _repo.DeleteMenuOptionAsync(optionId);
    }
}