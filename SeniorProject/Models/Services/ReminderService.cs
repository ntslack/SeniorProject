using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

namespace SeniorProject.Models.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public Task<List<ReminderDTO>> GetRemindersAsync(int userID) => _reminderRepository.GetRemindersAsync(userID);

        public Task<ReminderDTO> GetReminderByID(int reminderID) => _reminderRepository.GetReminderByID(reminderID);

        public Task<ReminderDTO> CreateReminderAsync(ReminderDTO reminderDTO) => _reminderRepository.CreateReminderAsync(reminderDTO);

        public Task<ReminderDTO> UpdateReminderAsync(ReminderDTO reminderDTO) => _reminderRepository.UpdateReminderAsync(reminderDTO);

        public Task<int> DeleteReminderAsync(ReminderDTO reminderDTO) => _reminderRepository.DeleteReminderAsync(reminderDTO);
    }
}
