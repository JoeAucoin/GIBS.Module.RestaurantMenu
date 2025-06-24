using GIBS.Module.RestaurantMenu.Models;
using Microsoft.EntityFrameworkCore;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oqtane.Shared;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class MenuCategoryRepository : IMenuCategoryRepository, ITransientService
    {
        private readonly IDbContextFactory<RestaurantMenuContext> _factory;
        private readonly ILogManager _logger;

        public MenuCategoryRepository(IDbContextFactory<RestaurantMenuContext> factory, ILogManager logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public IEnumerable<MenuCategory> GetMenuCategories(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            var categories = from category in db.MenuCategory
                             join menu in db.RestaurantMenu on category.RestaurantMenuId equals menu.RestaurantMenuId
                             where category.ModuleId == moduleId
                             orderby menu.SortOrder, category.SortOrder
                             select category;
            return categories.ToList();
        }

        public async Task<MenuCategory> GetMenuCategoryAsync(int categoryId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuCategory.FirstOrDefaultAsync(item => item.CategoryId == categoryId && item.ModuleId == moduleId);
        }

        public async Task<MenuCategory> AddMenuCategoryAsync(MenuCategory menuCategory)
        {
            using var db = _factory.CreateDbContext();
            db.MenuCategory.Add(menuCategory);
            await db.SaveChangesAsync();
            return menuCategory;
        }

        public async Task<MenuCategory> UpdateMenuCategoryAsync(MenuCategory menuCategory)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(menuCategory).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return menuCategory;
        }

        public async Task DeleteMenuCategoryAsync(int categoryId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            MenuCategory menuCategory = await db.MenuCategory.FirstOrDefaultAsync(item => item.CategoryId == categoryId && item.ModuleId == moduleId);
            if (menuCategory != null)
            {
                db.MenuCategory.Remove(menuCategory);
                await db.SaveChangesAsync();
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Menu Category Deleted Successfully - CategoryId: {CategoryId}, ModuleId: {ModuleId}", categoryId, moduleId);
            }
            else
            {
                _logger.Log(LogLevel.Warning, this, LogFunction.Delete, "Attempted to delete non-existent Menu Category - CategoryId: {CategoryId}, ModuleId: {ModuleId}", categoryId, moduleId);
            }
        }

        public async Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuCategory
                .Include(c => c.RestaurantMenu)
                .Where(c => c.ModuleId == moduleId)
                .ToListAsync();
        }
    }
}