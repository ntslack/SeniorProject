using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsAsync(int userID)
        {
            List<EventDTO>? events = await _eventService.GetEventsAsync(userID);
            return Ok(events);
        }

        [HttpGet("{eventID}")]
        public async Task<IActionResult> GetEventByID(int eventID)
        {
            EventDTO? events = await _eventService.GetEventByID(eventID);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync([FromBody] EventDTO eventDTO)
        {
            var events = await _eventService.CreateEventAsync(eventDTO);
            return Ok(events);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync(EventDTO eventDTO)
        {
            var events = await _eventService.UpdateEventAsync(eventDTO);
            return Ok(events);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventAsync(EventDTO eventDTO)
        {
            int events = await _eventService.DeleteEventAsync(eventDTO);
            return Ok(events);
        }
    }
}
