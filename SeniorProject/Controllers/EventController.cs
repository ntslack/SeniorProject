using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
