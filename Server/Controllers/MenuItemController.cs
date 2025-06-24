using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Controllers;

namespace GIBS.Module.RestaurantMenu.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class MenuItemController : ModuleControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _menuItemService = menuItemService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<ActionResult<IEnumerable<MenuItem>>> Get(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return Ok(await _menuItemService.GetMenuItemsAsync(ModuleId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized MenuItem Get Attempt {ModuleId}", moduleid);
                return Forbid();
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<ActionResult<MenuItem>> Get(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemAsync(id, _entityId);
            if (menuItem != null && IsAuthorizedEntityId(EntityNames.Module, menuItem.ModuleId))
            {
                return Ok(menuItem);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized MenuItem Get Attempt {MenuItemId}", id);
                return Forbid();
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult<MenuItem>> Post([FromBody] MenuItem menuItem)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, menuItem.ModuleId))
            {
                var savedMenuItem = await _menuItemService.AddMenuItemAsync(menuItem);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Menu Item Added {MenuItem}", savedMenuItem);
                return Ok(savedMenuItem);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Menu Item Add Attempt {MenuItem}", menuItem);
                return Forbid();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult<MenuItem>> Put(int id, [FromBody] MenuItem menuItem)
        {
            if (ModelState.IsValid && menuItem.ItemId == id && IsAuthorizedEntityId(EntityNames.Module, menuItem.ModuleId))
            {
                var updatedMenuItem = await _menuItemService.UpdateMenuItemAsync(menuItem);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Menu Item Updated {MenuItem}", updatedMenuItem);
                return Ok(updatedMenuItem);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Menu Item Update Attempt {MenuItem}", menuItem);
                return Forbid();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult> Delete(int id)
        {
            if (IsAuthorizedEntityId(EntityNames.Module, _entityId))
            {
                await _menuItemService.DeleteMenuItemAsync(id, _entityId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Menu Item Deleted {MenuItemId}", id);
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Menu Item Delete Attempt {MenuItemId}", id);
                return Forbid();
            }
        }


        [HttpPut("{id}/attributes")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<IActionResult> UpdateMenuItemAttributes(int id, [FromBody] List<int> attributeIds)
        {
            if (IsAuthorizedEntityId(EntityNames.Module, _entityId))
            {
                await _menuItemService.UpdateMenuItemAttributesAsync(id, _entityId, attributeIds);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Attributes updated for Menu Item {MenuItemId}", id);
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Menu Item Attribute Update Attempt for {MenuItemId}", id);
                return Forbid();
            }
        }

        [HttpGet("attributes/{menuItemId}/{moduleId}")]
        public async Task<List<MenuItemAttribute>> GetMenuItemAttributesAsync(int menuItemId, int moduleId)
        {
            return await _menuItemService.GetMenuItemAttributesAsync(menuItemId, moduleId);
        }


        [HttpGet("withattributes/{moduleId}")]
        public async Task<List<MenuItem>> GetMenuItemsWithAttributesAsync(int moduleId)
        {
            return await _menuItemService.GetMenuItemsWithAttributesAsync(moduleId);
        }

    }
}