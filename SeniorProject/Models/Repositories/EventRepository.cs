using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class EventRepository : IEventRepository
    {
        DatabaseContext _dbcontext;
        public EventRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<EventDTO>> GetEventsAsync(int userID)
        {
            var events = (from e in _dbcontext.Event
                          select new EventDTO()
                          {
                              eventID = e.eventID,
                              eventTitle = e.eventTitle,
                              eventDescription = e.eventDescription,
                              eventStartTime = e.eventStartTime,
                              eventEndTime = e.eventEndTime,
                              eventCreationDate = e.eventCreationDate,
                              eventIsFavorited = e.eventIsFavorited,
                           }).ToListAsync();
            return await events;
        }
    }
}
