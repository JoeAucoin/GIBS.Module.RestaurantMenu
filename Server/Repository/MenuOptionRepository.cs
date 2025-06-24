using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class MenuOptionRepository : IMenuOptionRepository
    {
        private readonly RestaurantMenuContext _db;

        public MenuOptionRepository(RestaurantMenuContext db)
        {
            _db = db;
        }

        public async Task<List<MenuOption>> GetMenuOptionsByItemIdAsync(int itemId)
        {
            return await _db.MenuOption
                .Where(o => o.ItemId == itemId)
                .OrderBy(o => o.OptionName)
                .ToListAsync();
        }

        public async Task<MenuOption> GetMenuOptionAsync(int optionId)
        {
            return await _db.MenuOption
                .FirstOrDefaultAsync(o => o.OptionId == optionId);
        }

        public async Task<MenuOption> AddMenuOptionAsync(MenuOption option)
        {
            _db.MenuOption.Add(option);
            await _db.SaveChangesAsync();
            return option;
        }

        ////public async Task<MenuOption> UpdateMenuOptionAsync(MenuOption option)
        ////{
        ////    _db.MenuOption.Update(option);
        ////    await _db.SaveChangesAsync();
        ////    return option;
        ////}
        /// <summary>

        /// </summary>
        /// <param name="optionId"></param>
        /// <returns></returns>

        public async Task<MenuOption> UpdateMenuOptionAsync(MenuOption option)
        {
            // Fetch the tracked entity from the context
            var tracked = await _db.MenuOption.FirstOrDefaultAsync(o => o.OptionId == option.OptionId);
            if (tracked != null)
            {
                // Update only the properties you want to allow changes for
                tracked.OptionName = option.OptionName;
                tracked.OptionPrice = option.OptionPrice;
                tracked.OptionImage = option.OptionImage;
                tracked.MOCalories = option.MOCalories;
                tracked.MOProtein = option.MOProtein;
                tracked.MOFat = option.MOFat;
                tracked.MOCarbohydrates = option.MOCarbohydrates;
                tracked.MOSodium = option.MOSodium;
                tracked.IsActive = option.IsActive;
                tracked.ModifiedBy = option.ModifiedBy;
                tracked.ModifiedOn = option.ModifiedOn;
                // ... add any other fields you want to update

                await _db.SaveChangesAsync();
            }
            return tracked;
        }

        public async Task DeleteMenuOptionAsync(int optionId)
        {
            var option = await _db.MenuOption.FirstOrDefaultAsync(o => o.OptionId == optionId);
            if (option != null)
            {
                _db.MenuOption.Remove(option);
                await _db.SaveChangesAsync();
            }
        }
    }
}