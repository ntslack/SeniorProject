using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class FavNotesRepository : IFavNotesRepository
    {
        DatabaseContext _dbcontext;
        public FavNotesRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<NotesDTO>> GetFavNotesAsync(int userID)
        {
            var notes = (from n in _dbcontext.Note
                         join u in _dbcontext.User
                         on n.userID equals u.userID
                         where n.userID == userID
                         where n.noteIsFavorited == true
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

        public async Task<NotesDTO> FavoriteNote(NotesDTO notesDTO)
        {
            if (notesDTO == null)
            {
                throw new ArgumentNullException(nameof(notesDTO));
            }

            bool isFavorited = !notesDTO.noteIsFavorited;
            notesDTO.noteIsFavorited = isFavorited;

            _dbcontext.Update(notesDTO);
            await _dbcontext.SaveChangesAsync();

            return notesDTO;
        }
    }

    public class FavListsRepository : IFavListsRepository
    {
        DatabaseContext _dbcontext;
        public FavListsRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<ListDTO>> GetFavListsAsync(int userID)
        {
            var lists = (from l in _dbcontext.List
                         join u in _dbcontext.User
                         on l.userID equals u.userID
                         where l.userID == userID
                         where l.listIsFavorited == true
                         orderby l.listCreationDate descending
                         select new ListDTO()
                         {
                             listID = l.listID,
                             userID = l.userID,
                             listName = l.listName,
                             listDescription = l.listDescription,
                             listCreationDate = l.listCreationDate,
                             listIsFavorited = l.listIsFavorited
                         }).ToListAsync();
            return await lists;
        }

        public async Task<ListDTO> FavoriteList(ListDTO listDTO)
        {
            if (listDTO == null)
            {
                throw new ArgumentNullException(nameof(listDTO));
            }

            bool isFavorited = !listDTO.listIsFavorited;
            listDTO.listIsFavorited = isFavorited;

            _dbcontext.Update(listDTO);
            await _dbcontext.SaveChangesAsync();

            return listDTO;
        }
    }

    public class FavEventsRepository : IFavEventsRepository
    {
        DatabaseContext _dbcontext;
        public FavEventsRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<EventDTO>> GetFavEventsAsync(int userID)
        {
            var events = (from e in _dbcontext.Event
                          join u in _dbcontext.User
                          on e.userID equals u.userID
                          where e.userID == userID
                          where e.eventIsFavorited == true
                          orderby e.eventCreationDate descending
                          select new EventDTO()
                          {
                              eventID = e.eventID,
                              userID = e.userID,
                              eventTitle = e.eventTitle,
                              eventDescription = e.eventDescription,
                              eventStartTime = e.eventStartTime,
                              eventEndTime = e.eventEndTime,
                              eventCreationDate = e.eventCreationDate,
                              eventIsFavorited = e.eventIsFavorited,
                          }).ToListAsync();
            return await events;
        }

        public async Task<EventDTO> FavoriteEvent(EventDTO eventDTO)
        {
            if (eventDTO == null)
            {
                throw new ArgumentNullException(nameof(eventDTO));
            }

            bool isFavorited = !eventDTO.eventIsFavorited;
            eventDTO.eventIsFavorited = isFavorited;

            _dbcontext.Update(eventDTO);
            await _dbcontext.SaveChangesAsync();

            return eventDTO;
        }
    }

    public class Expense7DaysRepository : IExpense7DaysRepository
    {
        DatabaseContext _dbcontext;

        public Expense7DaysRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<double> GetExpensesLast7DaysAsync(int userID)
        {
            double expensesLast7Days = 0;
            expensesLast7Days = _dbcontext.Expense.Where(e => e.userID == userID && e.expenseCreationDate >= DateTime.Now.AddDays(-7)).Sum(e => e.expenseValue);
            return expensesLast7Days;
        }
    }

    public class Expense30DaysRepository : IExpense30DaysRepository
    {
        DatabaseContext _dbcontext;

        public Expense30DaysRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<double> GetExpensesLast30DaysAsync(int userID)
        {
            double expensesLast30Days = 0;
            expensesLast30Days = _dbcontext.Expense.Where(e => e.userID == userID && e.expenseCreationDate >= DateTime.Now.AddDays(-30)).Sum(e => e.expenseValue);
            return expensesLast30Days;
        }
    }
}
