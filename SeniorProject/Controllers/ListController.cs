using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
