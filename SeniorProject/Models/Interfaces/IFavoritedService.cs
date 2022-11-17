using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IFavNotesService
    {
        Task<List<NotesDTO>> GetFavNotesAsync(int userID);
        Task<NotesDTO> FavoriteNote(NotesDTO notesDTO);
    }

    public interface IFavListsService
    {
        Task<List<ListDTO>> GetFavListsAsync(int userID);
        Task<ListDTO> FavoriteList(ListDTO listDTO);
    }

    public interface IFavEventsService
    {
        Task<List<EventDTO>> GetFavEventsAsync(int userID);
        Task<EventDTO> FavoriteEvent(EventDTO eventDTO);
    }
}
