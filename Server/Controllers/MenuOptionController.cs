using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.RestaurantMenu.Models;
using GIBS.Module.RestaurantMenu.Services;

namespace GIBS.Module.RestaurantMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuOptionController : ControllerBase
    {
        private readonly IMenuOptionService _service;

        public MenuOptionController(IMenuOptionService service)
        {
            _service = service;
        }

        [HttpGet("byitem/{itemId}")]
        public async Task<List<MenuOption>> GetByItem(int itemId) =>
            await _service.GetMenuOptionsByItemIdAsync(itemId);

        [HttpGet("{id}")]
        public async Task<MenuOption> Get(int id) =>
            await _service.GetMenuOptionAsync(id);

        [HttpPost]
        public async Task<MenuOption> Post([FromBody] MenuOption option) =>
            await _service.AddMenuOptionAsync(option);

        [HttpPut("{id}")]
        public async Task<MenuOption> Put(int id, [FromBody] MenuOption option)
        {
            option.OptionId = id;
            return await _service.UpdateMenuOptionAsync(option);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id) =>
            await _service.DeleteMenuOptionAsync(id);
    }
}