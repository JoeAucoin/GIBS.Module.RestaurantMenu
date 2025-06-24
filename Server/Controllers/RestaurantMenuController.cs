using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.RestaurantMenu.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class RestaurantMenuController : ModuleControllerBase
    {
        private readonly IRestaurantMenuService _RestaurantMenuService;

        public RestaurantMenuController(IRestaurantMenuService RestaurantMenuService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _RestaurantMenuService = RestaurantMenuService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.RestaurantMenu>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _RestaurantMenuService.GetRestaurantMenusAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.RestaurantMenu> Get(int id, int moduleid)
        {
            Models.RestaurantMenu RestaurantMenu = await _RestaurantMenuService.GetRestaurantMenuAsync(id, moduleid);
            if (RestaurantMenu != null && IsAuthorizedEntityId(EntityNames.Module, RestaurantMenu.ModuleId))
            {
                return RestaurantMenu;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Get Attempt {RestaurantMenuId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.RestaurantMenu> Post([FromBody] Models.RestaurantMenu RestaurantMenu)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, RestaurantMenu.ModuleId))
            {
                RestaurantMenu = await _RestaurantMenuService.AddRestaurantMenuAsync(RestaurantMenu);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Post Attempt {RestaurantMenu}", RestaurantMenu);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                RestaurantMenu = null;
            }
            return RestaurantMenu;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.RestaurantMenu> Put(int id, [FromBody] Models.RestaurantMenu RestaurantMenu)
        {
            if (ModelState.IsValid && RestaurantMenu.RestaurantMenuId == id && IsAuthorizedEntityId(EntityNames.Module, RestaurantMenu.ModuleId))
            {
                RestaurantMenu = await _RestaurantMenuService.UpdateRestaurantMenuAsync(RestaurantMenu);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Put Attempt {RestaurantMenu}", RestaurantMenu);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                RestaurantMenu = null;
            }
            return RestaurantMenu;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.RestaurantMenu RestaurantMenu = await _RestaurantMenuService.GetRestaurantMenuAsync(id, moduleid);
            if (RestaurantMenu != null && IsAuthorizedEntityId(EntityNames.Module, RestaurantMenu.ModuleId))
            {
                await _RestaurantMenuService.DeleteRestaurantMenuAsync(id, RestaurantMenu.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized RestaurantMenu Delete Attempt {RestaurantMenuId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        [HttpGet("max-sortorder")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<int> GetMaxSortOrderForMenu([FromQuery] int moduleid)
        {
            if (!IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetMaxSortOrderForMenu Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return 0;
            }
            return await _RestaurantMenuService.GetMaxSortOrderForMenuAsync(moduleid);
        }
    }
}
