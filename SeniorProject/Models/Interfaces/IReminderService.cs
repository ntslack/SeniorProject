using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorProject.Models.DTOs;

namespace SeniorProject.Models.Interfaces
{
    public interface IReminderService
    {
        Task<ReminderDTO> GetReminderById();
    }
}
