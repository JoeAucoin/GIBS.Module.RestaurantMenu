using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Repository
{
    public class RestaurantMenuRepository : IRestaurantMenuRepository, ITransientService
    {
        private readonly IDbContextFactory<RestaurantMenuContext> _factory;

        public RestaurantMenuRepository(IDbContextFactory<RestaurantMenuContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.RestaurantMenu> GetRestaurantMenus(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.RestaurantMenu.Where(item => item.ModuleId == ModuleId).OrderBy(item => item.SortOrder).ToList();
        }

        public Models.RestaurantMenu GetRestaurantMenu(int RestaurantMenuId)
        {
            return GetRestaurantMenu(RestaurantMenuId, true);
        }

        public Models.RestaurantMenu GetRestaurantMenu(int RestaurantMenuId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.RestaurantMenu.Find(RestaurantMenuId);
            }
            else
            {
                return db.RestaurantMenu.AsNoTracking().FirstOrDefault(item => item.RestaurantMenuId == RestaurantMenuId);
            }
        }

        public Models.RestaurantMenu AddRestaurantMenu(Models.RestaurantMenu RestaurantMenu)
        {
            using var db = _factory.CreateDbContext();
            db.RestaurantMenu.Add(RestaurantMenu);
            db.SaveChanges();
            return RestaurantMenu;
        }

        public Models.RestaurantMenu UpdateRestaurantMenu(Models.RestaurantMenu RestaurantMenu)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(RestaurantMenu).State = EntityState.Modified;
            db.SaveChanges();
            return RestaurantMenu;
        }

        public void DeleteRestaurantMenu(int RestaurantMenuId)
        {
            using var db = _factory.CreateDbContext();
            Models.RestaurantMenu RestaurantMenu = db.RestaurantMenu.Find(RestaurantMenuId);
            db.RestaurantMenu.Remove(RestaurantMenu);
            db.SaveChanges();
        }

        public async Task<int> GetMaxSortOrderForMenuAsync(int moduleId)
        {
            using var db = _factory.CreateDbContext();
            // Assuming your entity is called RestaurantMenu and has a SortOrder property
            return await db.RestaurantMenu
                .Where(m => m.ModuleId == moduleId)
                .Select(m => (int?)m.SortOrder)
                .MaxAsync() ?? 0;
        }
    }
}
