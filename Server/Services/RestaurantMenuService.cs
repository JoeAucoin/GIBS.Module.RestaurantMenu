using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.RestaurantMenu.Repository;


namespace GIBS.Module.RestaurantMenu.Services
{
    public class RestaurantMenuService : IRestaurantMenuService
    {
        private readonly IRestaurantMenuRepository _RestaurantMenuRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public RestaurantMenuService(IRestaurantMenuRepository RestaurantMenuRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _RestaurantMenuRepository = RestaurantMenuRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.RestaurantMenu>> GetRestaurantMenusAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_RestaurantMenuRepository.GetRestaurantMenus(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.RestaurantMenu> GetRestaurantMenuAsync(int RestaurantMenuId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_RestaurantMenuRepository.GetRestaurantMenu(RestaurantMenuId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Get Attempt {RestaurantMenuId} {ModuleId}", RestaurantMenuId, ModuleId);
                return null;
            }
        }

        public Task<Models.RestaurantMenu> AddRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, RestaurantMenu.ModuleId, PermissionNames.Edit))
            {
                RestaurantMenu = _RestaurantMenuRepository.AddRestaurantMenu(RestaurantMenu);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "RestaurantMenu Added {RestaurantMenu}", RestaurantMenu);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Add Attempt {RestaurantMenu}", RestaurantMenu);
                RestaurantMenu = null;
            }
            return Task.FromResult(RestaurantMenu);
        }

        public Task<Models.RestaurantMenu> UpdateRestaurantMenuAsync(Models.RestaurantMenu RestaurantMenu)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, RestaurantMenu.ModuleId, PermissionNames.Edit))
            {
                RestaurantMenu = _RestaurantMenuRepository.UpdateRestaurantMenu(RestaurantMenu);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "RestaurantMenu Updated {RestaurantMenu}", RestaurantMenu);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Update Attempt {RestaurantMenu}", RestaurantMenu);
                RestaurantMenu = null;
            }
            return Task.FromResult(RestaurantMenu);
        }

        public Task DeleteRestaurantMenuAsync(int RestaurantMenuId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _RestaurantMenuRepository.DeleteRestaurantMenu(RestaurantMenuId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "RestaurantMenu Deleted {RestaurantMenuId}", RestaurantMenuId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Delete Attempt {RestaurantMenuId} {ModuleId}", RestaurantMenuId, ModuleId);
            }
            return Task.CompletedTask;
        }

        public Task<int> GetMaxSortOrderForMenuAsync(int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                return _RestaurantMenuRepository.GetMaxSortOrderForMenuAsync(moduleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetMaxSortOrderForMenuAsync Request For Module {ModuleId}", moduleId);
                return Task.FromResult(0);
            }

        }
    }
}
