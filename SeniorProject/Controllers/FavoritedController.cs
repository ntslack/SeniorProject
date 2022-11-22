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

    [Route("Home/expense7days")]
    [ApiController]
    public class Expense7DaysController : ControllerBase
    {
        private readonly IExpense7DaysService _expense7DaysService;

        public Expense7DaysController(IExpense7DaysService expense7DaysService)
        {
            _expense7DaysService = expense7DaysService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpensesLast7DaysAsync(int userID)
        {
            double expenses = await _expense7DaysService.GetExpensesLast7DaysAsync(userID);
            return Ok(expenses);
        }
    }

    [Route("Home/expense30days")]
    [ApiController]
    public class Expense30DaysController : ControllerBase
    {
        private readonly IExpense30DaysService _expense30DaysService;

        public Expense30DaysController(IExpense30DaysService expense30DaysService)
        {
            _expense30DaysService = expense30DaysService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpensesLast30DaysAsync(int userID)
        {
            double expenses = await _expense30DaysService.GetExpensesLast30DaysAsync(userID);
            return Ok(expenses);
        }
    }
}
