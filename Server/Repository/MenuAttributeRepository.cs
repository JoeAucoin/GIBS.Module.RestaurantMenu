﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class MenuAttributeRepository : IMenuAttributeRepository
    {
        private readonly RestaurantMenuContext _db;
        private readonly IDbContextFactory<RestaurantMenuContext> _factory;

        public MenuAttributeRepository(RestaurantMenuContext context, IDbContextFactory<RestaurantMenuContext> factory)
        {
            _db = context;
            _factory = factory;
        }

        public async Task<IEnumerable<MenuAttribute>> GetMenuAttributesAsync(int moduleId)
        {
            return await _db.MenuAttribute.Where(item => item.ModuleId == moduleId).AsNoTracking().ToListAsync();
        }

        public async Task<MenuAttribute> GetMenuAttributeAsync(int attributeId)
        {
            return await _db.MenuAttribute.FindAsync(attributeId);
        }

        public async Task<MenuAttribute> AddMenuAttributeAsync(MenuAttribute attribute)
        {
            _db.MenuAttribute.Add(attribute);
            await _db.SaveChangesAsync();
            return attribute;
        }

        public async Task<MenuAttribute> UpdateMenuAttributeAsync(MenuAttribute attribute)
        {
            _db.Entry(attribute).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return attribute;
        }

        public async Task DeleteMenuAttributeAsync(int attributeId)
        {
            var attribute = await _db.MenuAttribute.FindAsync(attributeId);
            if (attribute != null)
            {
                _db.MenuAttribute.Remove(attribute);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> GetMaxSortOrderForAttributeAsync(int attributeId, int moduleId)
        {
            using var db = _factory.CreateDbContext();
            return await db.MenuAttribute // Corrected from GIBSMenuAttribute to MenuAttribute
                .Where(a => a.ModuleId == moduleId)
                .Select(a => (int?)a.SortOrder)
                .MaxAsync() ?? 0;
        }
    }
}