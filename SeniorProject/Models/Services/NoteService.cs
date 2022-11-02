using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

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

        public Task<NotesDTO> GetNoteByID(int noteID) => _noteRepository.GetNoteByID(noteID);

        public Task<NotesDTO> CreateNoteAsync(NotesDTO notesDTO) => _noteRepository.CreateNoteAsync(notesDTO);

        public Task<NotesDTO> UpdateNoteAsync(NotesDTO notesDTO) => _noteRepository.UpdateNoteAsync(notesDTO);

        public Task<bool> DeleteNoteAsync(int noteID) => _noteRepository.DeleteNoteAsync(noteID);
    }
}
