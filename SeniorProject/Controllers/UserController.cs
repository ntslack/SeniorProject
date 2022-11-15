using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;
using SeniorProject.Models;

namespace SeniorProject.Controllers
{
    [Route("Home/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            List<UserAccount>? users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UserAccount account)
        {
            var users = await _userService.UpdateUserAsync(account);
            return Ok(users);
        }

        [HttpDelete("{userID}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int userID)
        {
            bool users = await _userService.DeleteUserAsync(userID);
            return Ok(users);
        }
    }
}
