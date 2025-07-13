using GIBS.Module.RestaurantMenu.Client.Services;
using GIBS.Module.RestaurantMenu.Services;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;

namespace GIBS.Module.RestaurantMenu.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRestaurantMenuService, RestaurantMenuService>();

            // Fix for CS0311: Ensure MenuCategoryService implements IMenuCategoryService
            services.AddScoped<IMenuCategoryService, MenuCategoryService>();

            services.AddScoped<IMenuOptionService, Client.Services.MenuOptionService>();

        }
    }
}
