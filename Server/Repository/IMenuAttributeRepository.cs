using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public interface IMenuAttributeRepository
    {
        Task<IEnumerable<MenuAttribute>> GetMenuAttributesAsync(int moduleId);
        Task<MenuAttribute> GetMenuAttributeAsync(int attributeId);
        Task<MenuAttribute> AddMenuAttributeAsync(MenuAttribute attribute);
        Task<MenuAttribute> UpdateMenuAttributeAsync(MenuAttribute attribute);
        Task DeleteMenuAttributeAsync(int attributeId);
    }
}