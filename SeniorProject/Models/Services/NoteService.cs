using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteService? _noteService;

        public Task<List<NotesDTO>> GetNotesAsync(int userID)
        {
            return _noteService.GetNotesAsync(userID);
        }
    }
}
