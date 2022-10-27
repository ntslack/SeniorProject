using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models.Entities;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SeniorProject.Models.Repositories
{
    public class NoteRepository : INoteRepository
    {
        DatabaseContext _dbcontext;
        public NoteRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public DbSet<NoteEntity> Notes { get; set; }


        public async Task<List<NotesDTO>> GetNotesAsync(int userID)
        {
            var notes = (from n in _dbcontext.Note
                         join u in _dbcontext.User
                         on n.userID equals u.userID
                         where n.userID == userID
                         orderby n.noteCreationDate descending
                         select new NotesDTO()
                         {
                             noteID = n.noteID,
                             userID = n.userID,
                             noteTitle = n.noteTitle,
                             noteValue = n.noteValue,
                             noteCreationDate = n.noteCreationDate,
                             noteIsFavorited = n.noteIsFavorited
                         }).ToListAsync();
            return await notes;
        }
        public async Task<NotesDTO> CreateNoteAsync(NotesDTO notesDTO)
        {
            await _dbcontext.AddAsync(notesDTO);
            await _dbcontext.SaveChangesAsync();

            return notesDTO;
        }
    }
}
