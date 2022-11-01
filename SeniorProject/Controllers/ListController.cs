using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListsAsync(int userID)
        {
            List<ListDTO>? lists = await _listService.GetListsAsync(userID);
            return Ok(lists);
        }

        [HttpGet("{listID}")]
        public async Task<IActionResult> GetListByID(int listID)
        {
            ListDTO? list = await _listService.GetListByID(listID);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateListAsync([FromBody] ListDTO listDTO)
        {
            var list = await _listService.CreateListAsync(listDTO);
            return Ok(list);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateListAsync(ListDTO listDTO)
        {
            var list = await _listService.UpdateListAsync(listDTO);
            return Ok(list);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListAsync(ListDTO listDTO)
        {
            int list = await _listService.DeleteListAsync(listDTO);
            return Ok(list);
        }
    }
}
