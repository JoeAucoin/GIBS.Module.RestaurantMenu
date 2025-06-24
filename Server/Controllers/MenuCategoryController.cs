using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oqtane.Controllers;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Shared;
using System;
using System.Collections.Generic;
using System.Linq; // Required for .Count() if used in logging
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class MenuCategoryController : ModuleControllerBase
    {
        private readonly IMenuCategoryService _menuCategoryService;
        // ILogManager is already available via ModuleControllerBase as _logger

        public MenuCategoryController(IMenuCategoryService menuCategoryService, ILogManager logger, IHttpContextAccessor accessor)
            : base(logger, accessor) // Pass logger to base
        {
            _menuCategoryService = menuCategoryService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<MenuCategory>> Get(string moduleid)
        {
            _logger.Log(LogLevel.Information, this, LogFunction.Read, "MenuCategoryController: Get method entered for moduleid: {ModuleIdString}", moduleid);
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                _logger.Log(LogLevel.Information, this, LogFunction.Read, "MenuCategoryController: Authorized. Calling _menuCategoryService.GetMenuCategoriesAsync for ModuleId: {ModuleIdInt}", ModuleId);
                var result = await _menuCategoryService.GetMenuCategoriesAsync(ModuleId);
                if (result == null)
                {
                    _logger.Log(LogLevel.Warning, this, LogFunction.Read, "MenuCategoryController: _menuCategoryService.GetMenuCategoriesAsync returned null for ModuleId: {ModuleIdInt}", ModuleId);
                }
                else
                {
                    _logger.Log(LogLevel.Information, this, LogFunction.Read, "MenuCategoryController: _menuCategoryService.GetMenuCategoriesAsync returned {Count} items for ModuleId: {ModuleIdInt}", result.Count(), ModuleId);
                }
                return result;
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "MenuCategoryController: Unauthorized MenuCategory Get Attempt {ModuleIdString}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        [HttpGet("{id}")] // Removed moduleid from route, pass as query param for consistency
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<ActionResult<MenuCategory>> Get(int id, [FromQuery] int moduleid)
        {
            if (!IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Get Attempt for MenuCategory {CategoryId} in Module {ModuleId}", id, moduleid);
                return Forbid();
            }
            var menuCategory = await _menuCategoryService.GetMenuCategoryAsync(id, moduleid);
            if (menuCategory == null)
            {
                _logger.Log(LogLevel.Warning, this, LogFunction.Read, "MenuCategory Get Failed - Not Found. CategoryId {CategoryId}, ModuleId {ModuleId}", id, moduleid);
                return NotFound();
            }
            return Ok(menuCategory);
        }

        // POST api/<controller>?moduleid=x
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult<MenuCategory>> Post([FromBody] MenuCategory menuCategory, [FromQuery] int moduleid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (menuCategory.ModuleId != moduleid || !IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized MenuCategory Post Attempt or ModuleId mismatch. Provided ModuleId {ProvidedModuleId}, Category ModuleId {CategoryModuleId}", moduleid, menuCategory.ModuleId);
                return Forbid();
            }
            var addedMenuCategory = await _menuCategoryService.AddMenuCategoryAsync(menuCategory);
            return CreatedAtAction(nameof(Get), new { id = addedMenuCategory.CategoryId, moduleid = addedMenuCategory.ModuleId }, addedMenuCategory);
        }

        // PUT api/<controller>/5?moduleid=x
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult<MenuCategory>> Put(int id, [FromBody] MenuCategory menuCategory, [FromQuery] int moduleid)
        {
            if (!ModelState.IsValid || id != menuCategory.CategoryId) return BadRequest(ModelState);
            if (menuCategory.ModuleId != moduleid || !IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized MenuCategory Put Attempt or ModuleId mismatch. Provided ModuleId {ProvidedModuleId}, Category ModuleId {CategoryModuleId}", moduleid, menuCategory.ModuleId);
                return Forbid();
            }
            var updatedMenuCategory = await _menuCategoryService.UpdateMenuCategoryAsync(menuCategory);
            if (updatedMenuCategory == null) return NotFound(); // Or handle as appropriate
            return Ok(updatedMenuCategory);
        }

        // DELETE api/<controller>/5?moduleid=x
        [HttpDelete("{id}")] // Removed moduleid from route, pass as query param
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<IActionResult> Delete(int id, [FromQuery] int moduleid)
        {
            if (!IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized MenuCategory Delete Attempt for CategoryId {CategoryId} in Module {ModuleId}", id, moduleid);
                return Forbid();
            }

            // Optional: Check if the item exists before deleting, though the service/repo might handle this.
            // var menuCategory = await _menuCategoryService.GetMenuCategoryAsync(id, moduleid);
            // if (menuCategory == null)
            // {
            //     _logger.Log(LogLevel.Warning, this, LogFunction.Delete, "MenuCategory Delete Failed - Not Found. CategoryId {CategoryId}, ModuleId {ModuleId}", id, moduleid);
            //     return NotFound();
            // }

            await _menuCategoryService.DeleteMenuCategoryAsync(id, moduleid);
            _logger.Log(LogLevel.Information, this, LogFunction.Delete, "MenuCategoryController: Menu Category Deletion Processed - CategoryId: {CategoryId}, ModuleId: {ModuleId}", id, moduleid);
            return NoContent(); // Standard response for a successful DELETE
        }

        [HttpGet("withmenu/{moduleId}")]
        public async Task<List<MenuCategory>> GetMenuCategoriesWithMenuAsync(int moduleId)
        {
            return await _menuCategoryService.GetMenuCategoriesWithMenuAsync(moduleId);
        }

        // Properties removed for brevity as they are not used in the Get method
        // and can cause confusion if not correctly populated.
        // If these are needed for other actions, ensure they are set appropriately.
    }
}