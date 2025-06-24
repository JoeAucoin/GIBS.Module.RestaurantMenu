using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Services
{
    public interface IRestaurantMenuService 
    {
        Task<List<Models.RestaurantMenu>> GetRestaurantMenusAsync(int ModuleId);

        Task<Models.RestaurantMenu> GetRestaurantMenuAsync(int RestaurantMenuId, int ModuleId);

        Task<Models.RestaurantMenu> AddRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu);

        Task<Models.RestaurantMenu> UpdateRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu);

        Task DeleteRestaurantMenuAsync(int RestaurantMenuId, int ModuleId);
    }
}
