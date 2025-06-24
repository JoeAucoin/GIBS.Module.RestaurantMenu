using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public interface IRestaurantMenuRepository
    {
        IEnumerable<Models.RestaurantMenu> GetRestaurantMenus(int ModuleId);
        Models.RestaurantMenu GetRestaurantMenu(int RestaurantMenuId);
        Models.RestaurantMenu GetRestaurantMenu(int RestaurantMenuId, bool tracking);
        Models.RestaurantMenu AddRestaurantMenu(Models.RestaurantMenu RestaurantMenu);
        Models.RestaurantMenu UpdateRestaurantMenu(Models.RestaurantMenu RestaurantMenu);
        void DeleteRestaurantMenu(int RestaurantMenuId);
    }
}
