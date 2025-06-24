using GIBS.Module.RestaurantMenu.Repository;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Security;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;
using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.RestaurantMenu.Services
{
    public class MenuAttributeService : IMenuAttributeService, ITransientService
    {
        private readonly IMenuAttributeRepository _menuAttributeRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogManager _logger;
        private readonly Alias _alias;

        public MenuAttributeService(IMenuAttributeRepository menuAttributeRepository, IUserPermissions userPermissions, IHttpContextAccessor accessor, ILogManager logger, ITenantManager tenantManager)
        {
            _menuAttributeRepository = menuAttributeRepository;
            _userPermissions = userPermissions;
            _accessor = accessor;
            _logger = logger;
            _alias = tenantManager.GetAlias();
        }

        public async Task<List<MenuAttribute>> GetMenuAttributesAsync(int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                var attributes = await _menuAttributeRepository.GetMenuAttributesAsync(moduleId);
                return attributes.ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetMenuAttributesAsync Request For Module {ModuleId}", moduleId);
                return null;
            }
        }

        public async Task<MenuAttribute> GetMenuAttributeAsync(int attributeId, int moduleId)
        {
            var attribute = await _menuAttributeRepository.GetMenuAttributeAsync(attributeId);
            if (attribute != null && attribute.ModuleId == moduleId && _userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return attribute;
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetMenuAttributeAsync Request For Attribute {AttributeId} And Module {ModuleId}", attributeId, moduleId);
                return null;
            }
        }

        public async Task<MenuAttribute> AddMenuAttributeAsync(MenuAttribute attribute)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, attribute.ModuleId, PermissionNames.Edit))
            {
                return await _menuAttributeRepository.AddMenuAttributeAsync(attribute);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized AddMenuAttributeAsync Request For Module {ModuleId}", attribute.ModuleId);
                return null;
            }
        }

        public async Task<MenuAttribute> UpdateMenuAttributeAsync(MenuAttribute attribute)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, attribute.ModuleId, PermissionNames.Edit))
            {
                return await _menuAttributeRepository.UpdateMenuAttributeAsync(attribute);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized UpdateMenuAttributeAsync Request For Module {ModuleId}", attribute.ModuleId);
                return null;
            }
        }

        public async Task DeleteMenuAttributeAsync(int attributeId, int moduleId)
        {
            var attribute = await _menuAttributeRepository.GetMenuAttributeAsync(attributeId);
            if (attribute != null && attribute.ModuleId == moduleId && _userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                await _menuAttributeRepository.DeleteMenuAttributeAsync(attributeId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized DeleteMenuAttributeAsync Request For Attribute {AttributeId} And Module {ModuleId}", attributeId, moduleId);
            }
        }
    }
}