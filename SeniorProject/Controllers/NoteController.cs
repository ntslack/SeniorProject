using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Controllers
{
    [Route("/Home/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotesAsync(int userID)
        {
            //userID = HttpContext.Session.GetInt32("userID");
            userID = 1;
            List<NotesDTO>? notes = await _noteService.GetNotesAsync(userID);
            return Ok(notes);
        }
    }
}
