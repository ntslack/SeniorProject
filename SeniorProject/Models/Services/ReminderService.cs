using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderService _reminderService;

        public Task<ReminderDTO> GetReminderById()
        {
            return _reminderService.GetReminderById();
        }
    }
}
