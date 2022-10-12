using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task<List<NotesDTO>> GetNotesAsync(int userID) => _noteRepository.GetNotesAsync(userID);



        //private readonly INoteService? _noteService;

        //public Task<List<NotesDTO>> GetNotesAsync(int userID)
        //{
        //    return _noteService.GetNotesAsync(userID);
        //}
    }
}
