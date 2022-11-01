using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/reminders")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRemindersAsync(int userID)
        {
            List<ReminderDTO>? reminders = await _reminderService.GetRemindersAsync(userID);
            return Ok(reminders);
        }

        [HttpGet("{reminderID}")]
        public async Task<IActionResult> GetReminderByID(int reminderID)
        {
            ReminderDTO? reminder = await _reminderService.GetReminderByID(reminderID);
            return Ok(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminderAsync([FromBody] ReminderDTO reminderDTO)
        {
            var reminder = await _reminderService.CreateReminderAsync(reminderDTO);
            return Ok(reminder);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReminderAsync(ReminderDTO reminderDTO)
        {
            var reminder = await _reminderService.UpdateReminderAsync(reminderDTO);
            return Ok(reminder);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReminderAsync(ReminderDTO reminderDTO)
        {
            int reminder = await _reminderService.DeleteReminderAsync(reminderDTO);
            return Ok(reminder);
        }
    }
}
