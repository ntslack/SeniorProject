using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Task<List<EventDTO>> GetEventsAsync(int userID) => _eventRepository.GetEventsAsync(userID);

        public Task<EventDTO> GetEventByID(int eventID) => _eventRepository.GetEventByID(eventID);

        public Task<EventDTO> CreateEventAsync(EventDTO eventDTO) => _eventRepository.CreateEventAsync(eventDTO);

        public Task<EventDTO> UpdateEventAsync(EventDTO eventDTO) => _eventRepository.UpdateEventAsync(eventDTO);

        public Task<int> DeleteEventAsync(EventDTO eventDTO) => _eventRepository.DeleteEventAsync(eventDTO);
    }
}
