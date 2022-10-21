using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    [Route("/api/reminders")]
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
            userID = 1;
            List<ReminderDTO>? reminders = await _reminderService.GetRemindersAsync(userID);
            return Ok(reminders);
        }
    }
}
