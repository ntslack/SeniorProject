using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IReminderRepository
    {
        Task<List<ReminderDTO>> GetRemindersAsync(int userID);

        Task<ReminderDTO> GetReminderByID(int reminderID);

        Task<ReminderDTO> CreateReminderAsync(ReminderDTO reminderDTO);

        Task<ReminderDTO> UpdateReminderAsync(ReminderDTO reminderDTO);

        Task<bool> DeleteReminderAsync(int reminderID);
    }
}
