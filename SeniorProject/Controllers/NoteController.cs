using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/notes")]
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
            List<NotesDTO>? notes = await _noteService.GetNotesAsync(userID);
            return Ok(notes);
        }

        [HttpGet("{noteID}")]
        public async Task<IActionResult> GetNoteByID(int noteID)
        {
            NotesDTO? note = await _noteService.GetNoteByID(noteID);
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNoteAsync([FromBody] NotesDTO notesDTO)
        {
            var note = await _noteService.CreateNoteAsync(notesDTO);
            return Ok(note);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNoteAsync(NotesDTO notesDTO)
        {
            var note = await _noteService.UpdateNoteAsync(notesDTO);
            return Ok(note);
        }

        [HttpDelete("{noteID}")]
        public async Task<IActionResult> DeleteNoteAsync(int noteID)
        {
            bool note = await _noteService.DeleteNoteAsync(noteID);
            return Ok(note);
        }
    }
}
