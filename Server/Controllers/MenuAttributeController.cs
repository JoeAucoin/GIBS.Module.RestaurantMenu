using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Services;
using System.Threading.Tasks;

namespace GIBS.Module.RestaurantMenu.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class MenuAttributeController : Controller
    {
        private readonly IMenuAttributeService _menuAttributeService;
        private readonly ILogManager _logger;

        public MenuAttributeController(IMenuAttributeService menuAttributeService, ILogManager logger)
        {
            _menuAttributeService = menuAttributeService;
            _logger = logger;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<MenuAttribute>> Get(string moduleid)
        {
            return await _menuAttributeService.GetMenuAttributesAsync(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<MenuAttribute> Get(int id, [FromQuery] int moduleid)
        {
            return await _menuAttributeService.GetMenuAttributeAsync(id, moduleid);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<MenuAttribute> Post([FromBody] MenuAttribute attribute)
        {
            if (ModelState.IsValid)
            {
                attribute = await _menuAttributeService.AddMenuAttributeAsync(attribute);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Attribute Added {attribute}", attribute);
            }
            return attribute;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<MenuAttribute> Put(int id, [FromBody] MenuAttribute attribute)
        {
            if (ModelState.IsValid && attribute.AttributeId == id)
            {
                attribute = await _menuAttributeService.UpdateMenuAttributeAsync(attribute);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Attribute Updated {attribute}", attribute);
            }
            return attribute;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, [FromQuery] int moduleid)
        {
            await _menuAttributeService.DeleteMenuAttributeAsync(id, moduleid);
            _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Attribute Deleted {AttributeId}", id);
        }
    }
}