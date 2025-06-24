using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.RestaurantMenu.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Manager
{
    public class RestaurantMenuManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IRestaurantMenuRepository _RestaurantMenuRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public RestaurantMenuManager(IRestaurantMenuRepository RestaurantMenuRepository, IDBContextDependencies DBContextDependencies)
        {
            _RestaurantMenuRepository = RestaurantMenuRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new RestaurantMenuContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new RestaurantMenuContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.RestaurantMenu> RestaurantMenus = _RestaurantMenuRepository.GetRestaurantMenus(module.ModuleId).ToList();
            if (RestaurantMenus != null)
            {
                content = JsonSerializer.Serialize(RestaurantMenus);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.RestaurantMenu> RestaurantMenus = null;
            if (!string.IsNullOrEmpty(content))
            {
                RestaurantMenus = JsonSerializer.Deserialize<List<Models.RestaurantMenu>>(content);
            }
            if (RestaurantMenus != null)
            {
                foreach(var RestaurantMenu in RestaurantMenus)
                {
                    _RestaurantMenuRepository.AddRestaurantMenu(new Models.RestaurantMenu { ModuleId = module.ModuleId, Name = RestaurantMenu.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var RestaurantMenu in _RestaurantMenuRepository.GetRestaurantMenus(pageModule.ModuleId))
           {
               if (RestaurantMenu.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSRestaurantMenu",
                       EntityId = RestaurantMenu.RestaurantMenuId.ToString(),
                       Title = RestaurantMenu.Name,
                       Body = RestaurantMenu.Name,
                       ContentModifiedBy = RestaurantMenu.ModifiedBy,
                       ContentModifiedOn = RestaurantMenu.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
