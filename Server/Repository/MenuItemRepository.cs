using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oqtane.Modules;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class MenuItemRepository : IMenuItemRepository, ITransientService
    {
        private readonly IDbContextFactory<RestaurantMenuContext> _factory;

        public MenuItemRepository(IDbContextFactory<RestaurantMenuContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<MenuItem> GetMenuItems(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            var query = from menuItem in db.MenuItem
                        join menu in db.RestaurantMenu on menuItem.RestaurantMenuId equals menu.RestaurantMenuId
                        join category in db.MenuCategory on menuItem.CategoryId equals category.CategoryId
                        where menuItem.ModuleId == moduleId
                        orderby menu.SortOrder, category.SortOrder, menuItem.SortOrder
                        select menuItem;

            return query.AsNoTracking().ToList();
        }

        // Add the implementation for the new method
        public IEnumerable<MenuItem> GetMenuItemsByCategory(int categoryId)
        {
            using var db = _factory.CreateDbContext();
            return db.MenuItem.AsNoTracking()
                .Where(item => item.CategoryId == categoryId)
                .OrderBy(item => item.SortOrder)
                .ToList();
        }

        public async Task<MenuItem> GetMenuItemAsync(int itemId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuItem.AsNoTracking()
                .FirstOrDefaultAsync(item => item.ItemId == itemId && item.ModuleId == moduleId);
        }

        public async Task<MenuItem> AddMenuItemAsync(MenuItem menuItem)
        {
            using var db = _factory.CreateDbContext();
            db.MenuItem.Add(menuItem);
            await db.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(menuItem).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return menuItem;
        }

        public async Task DeleteMenuItemAsync(int itemId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            var menuItem = await db.MenuItem
                .FirstOrDefaultAsync(item => item.ItemId == itemId && item.ModuleId == moduleId);

            if (menuItem != null)
            {
                db.MenuItem.Remove(menuItem);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateMenuItemAttributesAsync(int itemId, int moduleId, List<int> attributeIds)
        {
            using var db = _factory.CreateDbContext();

            var existingAttributes = db.MenuItemAttribute.Where(a => a.ItemId == itemId);
            db.MenuItemAttribute.RemoveRange(existingAttributes);

            foreach (var attributeId in attributeIds)
            {
                db.MenuItemAttribute.Add(new MenuItemAttribute
                {
                    ItemId = itemId,
                    AttributeId = attributeId
                });
            }

            await db.SaveChangesAsync();
        }

        public async Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuItemAttribute
                .AsNoTracking()
                .Where(a => a.ItemId == menuItemId && a.MenuItem.ModuleId == moduleId)
                .ToListAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuItem
                .Include(m => m.MenuItemAttributes)
                    .ThenInclude(a => a.MenuAttribute)
                .Include(m => m.MenuOptions)
                .Where(m => m.ModuleId == moduleId)
                .OrderBy(m => m.SortOrder)
                .ThenBy(m => m.ItemName)
                .ToListAsync();
        }
    }
}