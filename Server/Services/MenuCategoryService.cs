using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Repository;
using GIBS.Module.RestaurantMenu.Services;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Security;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedRestaurantMenuModels = GIBS.Module.RestaurantMenu.Models;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuCategoryService : IMenuCategoryService, ITransientService
    {
        private readonly IMenuCategoryRepository _menuCategoryRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger; // Already injected
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public MenuCategoryService(IMenuCategoryRepository menuCategoryRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _menuCategoryRepository = menuCategoryRepository;
            _userPermissions = userPermissions;
            _logger = logger; // Already assigned
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<SharedRestaurantMenuModels.MenuCategory>> GetMenuCategoriesAsync(int moduleId)
        {
            _logger.Log(LogLevel.Information, this, LogFunction.Read, "ServerMenuCategoryService: GetMenuCategoriesAsync entered for ModuleId: {ModuleId}", moduleId);
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
             //   _logger.Log(LogLevel.Information, this, LogFunction.Read, "ServerMenuCategoryService: Authorized. Calling _menuCategoryRepository.GetMenuCategories for ModuleId: {ModuleId}", moduleId);
                var menuCategories = _menuCategoryRepository.GetMenuCategories(moduleId).ToList();

                if (menuCategories == null)
                {
                    _logger.Log(LogLevel.Warning, this, LogFunction.Read, "ServerMenuCategoryService: _menuCategoryRepository.GetMenuCategories returned null for ModuleId: {ModuleId}", moduleId);
                }
                else
                {
                   // _logger.Log(LogLevel.Information, this, LogFunction.Read, "ServerMenuCategoryService: _menuCategoryRepository.GetMenuCategories returned {Count} items for ModuleId: {ModuleId}", menuCategories.Count(), moduleId);
                    // Optional: Log details of first item if exists
                    if (menuCategories.Any())
                    {
                        var firstCat = menuCategories.First();
                        _logger.Log(LogLevel.Debug, this, LogFunction.Read, "ServerMenuCategoryService: First category details - ID: {CategoryId}, Name: '{CategoryName}', Description: '{CategoryDescription}'", firstCat.CategoryId, firstCat.CategoryName, firstCat.CategoryDescription);
                    }
                }
                return await Task.FromResult(menuCategories);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "ServerMenuCategoryService: Unauthorized MenuCategories Get Attempt {ModuleId}", moduleId);
                return null;
            }
        }

        public async Task<SharedRestaurantMenuModels.MenuCategory> GetMenuCategoryAsync(int categoryId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return await _menuCategoryRepository.GetMenuCategoryAsync(categoryId, moduleId);
            }
            _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetMenuCategoryAsync Attempt for CategoryId {CategoryId}, ModuleId {ModuleId}", categoryId, moduleId);
            return null;
        }

        public async Task<SharedRestaurantMenuModels.MenuCategory> AddMenuCategoryAsync(SharedRestaurantMenuModels.MenuCategory menuCategory)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, menuCategory.ModuleId, PermissionNames.Edit))
            {
                return await _menuCategoryRepository.AddMenuCategoryAsync(menuCategory);
            }
            _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized AddMenuCategoryAsync Attempt for ModuleId {ModuleId}", menuCategory.ModuleId);
            return null;
        }

        public async Task<SharedRestaurantMenuModels.MenuCategory> UpdateMenuCategoryAsync(SharedRestaurantMenuModels.MenuCategory menuCategory)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, menuCategory.ModuleId, PermissionNames.Edit))
            {
                return await _menuCategoryRepository.UpdateMenuCategoryAsync(menuCategory);
            }
            _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized UpdateMenuCategoryAsync Attempt for CategoryId {CategoryId}, ModuleId {ModuleId}", menuCategory.CategoryId, menuCategory.ModuleId);
            return null;
        }

        public async Task DeleteMenuCategoryAsync(int categoryId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                await _menuCategoryRepository.DeleteMenuCategoryAsync(categoryId, moduleId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "ServerMenuCategoryService: Menu Category Deletion Authorized and Processed - CategoryId: {CategoryId}, ModuleId: {ModuleId}", categoryId, moduleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "ServerMenuCategoryService: Unauthorized DeleteMenuCategoryAsync Attempt for CategoryId {CategoryId}, ModuleId {ModuleId}", categoryId, moduleId);
                // Optionally, you could throw an exception here or return a status
            }
        }

        public async Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId)
        {
            return await _menuCategoryRepository.GetMenuCategoriesWithMenuAsync(moduleId);
        }

    }
}