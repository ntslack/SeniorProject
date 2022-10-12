using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class NoteRepository : INoteRepository
    {
        DatabaseContext _dbcontext;
        public NoteRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<NotesDTO>> GetNotesAsync(int userID)
        {
            if (userID == null)
            {
                throw new ArgumentNullException(nameof(userID));
            }
            //return _dbcontext.Note
            //    .Include(x => x.User)
            //    .ToList();


            var notes = (from n in _dbcontext.Note
                             //join u in _dbcontext.User
                             //on n.userID equals u.userID
                         where n.userID == userID
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
    }
}
