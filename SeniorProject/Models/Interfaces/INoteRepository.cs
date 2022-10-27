using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface INoteRepository
    {

        Task<List<NotesDTO>> GetNotesAsync(int userID);

        Task<NotesDTO> CreateNoteAsync(NotesDTO notesDTO);
    }
}
