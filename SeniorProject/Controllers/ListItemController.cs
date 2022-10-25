using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    [Route("Home/listitems")]
    [ApiController]
    public class ListItemController : ControllerBase
    {
        private readonly IListItemService _listItemService;

        public ListItemController(IListItemService listItemService)
        {
            _listItemService = listItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListItemsAsync(int listID)
        {
            List<ListItemDTO>? listItems = await _listItemService.GetListItemsAsync(listID);
            return Ok(listItems);
        }
    }
}
