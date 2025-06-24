using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.RestaurantMenu.Repository;

using GIBS.Module.RestaurantMenu.Services;
using Microsoft.EntityFrameworkCore;

namespace GIBS.Module.RestaurantMenu.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext
            services.AddDbContextFactory<RestaurantMenuContext>(opt => { }, ServiceLifetime.Transient);

            // Add custom services
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<IMenuItemRepository, MenuItemRepository>();
            services.AddTransient<IMenuCategoryService, MenuCategoryService>();
            services.AddTransient<IMenuCategoryRepository, MenuCategoryRepository>();
            services.AddTransient<IRestaurantMenuService, RestaurantMenuService>();
            services.AddTransient<IRestaurantMenuRepository, RestaurantMenuRepository>();
            services.AddTransient<IMenuAttributeService, MenuAttributeService>();
            services.AddTransient<IMenuAttributeRepository, MenuAttributeRepository>();
            services.AddTransient<IMenuOptionRepository, MenuOptionRepository>();
            services.AddScoped<IMenuOptionService, MenuOptionService>();
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // No custom configuration needed
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // No custom configuration needed
        }
    }
}