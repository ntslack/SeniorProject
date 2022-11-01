using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

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

        [HttpGet("{listItemID}")]
        public async Task<IActionResult> GetListItemByID(int listItemID)
        {
            ListItemDTO? listItem = await _listItemService.GetListItemByID(listItemID);
            return Ok(listItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateListItemAsync([FromBody] ListItemDTO listItemDTO)
        {
            var listItem = await _listItemService.CreateListItemAsync(listItemDTO);
            return Ok(listItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateListItemAsync(ListItemDTO listItemDTO)
        {
            var listItem = await _listItemService.UpdateListItemAsync(listItemDTO);
            return Ok(listItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListItemAsync(ListItemDTO listItemDTO)
        {
            int listItem = await _listItemService.DeleteListItemAsync(listItemDTO);
            return Ok(listItem);
        }
    }
}
