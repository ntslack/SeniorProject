using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/favnotes")]
    [ApiController]
    public class FavNotesController : ControllerBase
    {
        private readonly IFavNotesService _favNotesService;

        public FavNotesController(IFavNotesService favNotesService)
        {
            _favNotesService = favNotesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavNotesAsync(int userID)
        {
            List<NotesDTO>? notes = await _favNotesService.GetFavNotesAsync(userID);
            return Ok(notes);
        }

        [HttpPut]
        public async Task<IActionResult> FavoriteNote(NotesDTO notesDTO)
        {
            var note = await _favNotesService.FavoriteNote(notesDTO);
            return Ok(note);
        }
    }

    [Route("Home/favlists")]
    [ApiController]
    public class FavListsController : ControllerBase
    {
        private readonly IFavListsService _favListsService;

        public FavListsController(IFavListsService favListsService)
        {
            _favListsService = favListsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavListsAsync(int userID)
        {
            List<ListDTO>? lists = await _favListsService.GetFavListsAsync(userID);
            return Ok(lists);
        }

        [HttpPut]
        public async Task<IActionResult> FavoriteList(ListDTO listDTO)
        {
            var list = await _favListsService.FavoriteList(listDTO);
            return Ok(list);
        }
    }

    [Route("Home/favevents")]
    [ApiController]
    public class FavEventsController : ControllerBase
    {
        private readonly IFavEventsService _favEventsService;

        public FavEventsController(IFavEventsService favEventsService)
        {
            _favEventsService = favEventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavEventsAsync(int userID)
        {
            List<EventDTO>? events = await _favEventsService.GetFavEventsAsync(userID);
            return Ok(events);
        }

        [HttpPut]
        public async Task<IActionResult> FavoriteEvent(EventDTO eventDTO)
        {
            var events = await _favEventsService.FavoriteEvent(eventDTO);
            return Ok(events);
        }
    }
}
