using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IFavNotesRepository
    {
        Task<List<NotesDTO>> GetFavNotesAsync(int userID);
        Task<NotesDTO> FavoriteNote(NotesDTO note);
    }

    public interface IFavListsRepository
    {
        Task<List<ListDTO>> GetFavListsAsync(int userID);
        Task<ListDTO> FavoriteList(ListDTO listDTO);
    }

    public interface IFavEventsRepository
    {
        Task<List<EventDTO>> GetFavEventsAsync(int userID);
        Task<EventDTO> FavoriteEvent(EventDTO eventDTO);
    }
}
