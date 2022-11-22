using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

namespace SeniorProject.Models.Services
{
    public class FavNotesService : IFavNotesService
    {
        private readonly IFavNotesRepository _favNotesRepository;

        public FavNotesService(IFavNotesRepository favNotesRepository)
        {
            _favNotesRepository = favNotesRepository;
        }

        public Task<List<NotesDTO>> GetFavNotesAsync(int userID) => _favNotesRepository.GetFavNotesAsync(userID);
        public Task<NotesDTO> FavoriteNote(NotesDTO notesDTO) => _favNotesRepository.FavoriteNote(notesDTO);
    }

    public class FavListsService : IFavListsService
    {
        private readonly IFavListsRepository _favListsRepository;

        public FavListsService(IFavListsRepository favListsRepository)
        {
            _favListsRepository = favListsRepository;
        }

        public Task<List<ListDTO>> GetFavListsAsync(int userID) => _favListsRepository.GetFavListsAsync(userID);
        public Task<ListDTO> FavoriteList(ListDTO listDTO) => _favListsRepository.FavoriteList(listDTO);
    }

    public class FavEventsService : IFavEventsService
    {
        private readonly IFavEventsRepository _favEventsRepository;

        public FavEventsService(IFavEventsRepository favEventsRepository)
        {
            _favEventsRepository = favEventsRepository;
        }

        public Task<List<EventDTO>> GetFavEventsAsync(int userID) => _favEventsRepository.GetFavEventsAsync(userID);
        public Task<EventDTO> FavoriteEvent(EventDTO eventDTO) => _favEventsRepository.FavoriteEvent(eventDTO);
    }

    public class Expense7DaysService : IExpense7DaysService
    {
        private readonly IExpense7DaysRepository _expense7DaysRepository;

        public Expense7DaysService(IExpense7DaysRepository expense7DaysRepository)
        {
            _expense7DaysRepository = expense7DaysRepository;
        }

        public Task<double> GetExpensesLast7DaysAsync(int userID) => _expense7DaysRepository.GetExpensesLast7DaysAsync(userID);
    }

    public class Expense30DaysService : IExpense30DaysService
    {
        private readonly IExpense30DaysRepository _expense30DaysRepository;

        public Expense30DaysService(IExpense30DaysRepository expense30DaysRepository)
        {
            _expense30DaysRepository = expense30DaysRepository;
        }

        public Task<double> GetExpensesLast30DaysAsync(int userID) => _expense30DaysRepository.GetExpensesLast30DaysAsync(userID);
    }
}
