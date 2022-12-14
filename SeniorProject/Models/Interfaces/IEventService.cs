using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDTO>> GetEventsAsync(int userID);

        Task<EventDTO> GetEventByID(int eventID);

        Task<EventDTO> CreateEventAsync(EventDTO eventDTO);

        Task<EventDTO> UpdateEventAsync(EventDTO eventDTO);

        Task<bool> DeleteEventAsync(int eventID);
    }
}
