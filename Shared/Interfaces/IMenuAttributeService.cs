using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Services
{
    public interface IMenuAttributeService
    {
        Task<List<MenuAttribute>> GetMenuAttributesAsync(int moduleId);
        Task<MenuAttribute> GetMenuAttributeAsync(int attributeId, int moduleId);
        Task<MenuAttribute> AddMenuAttributeAsync(MenuAttribute attribute);
        Task<MenuAttribute> UpdateMenuAttributeAsync(MenuAttribute attribute);
        Task DeleteMenuAttributeAsync(int attributeId, int moduleId);
    }
}