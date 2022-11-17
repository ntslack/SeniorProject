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
                          join u in _dbcontext.User
                          on e.userID equals u.userID
                          where e.userID == userID
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

        public async Task<EventDTO> GetEventByID(int eventID)
        {
            var events = await (from e in _dbcontext.Event
                              where e.eventID == eventID
                              select new EventDTO()
                              {
                                  eventID = e.eventID,
                                  eventTitle = e.eventTitle,
                                  eventDescription = e.eventDescription,
                                  eventStartTime = e.eventStartTime,
                                  eventEndTime = e.eventEndTime
                              }).SingleOrDefaultAsync();
            return events;
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO eventDTO)
        {
            await _dbcontext.AddAsync(eventDTO);
            await _dbcontext.SaveChangesAsync();

            return eventDTO;
        }

        public async Task<EventDTO> UpdateEventAsync(EventDTO eventDTO)
        {
            if (eventDTO == null)
            {
                throw new ArgumentNullException(nameof(eventDTO));
            }

            _dbcontext.Update(eventDTO);
            await _dbcontext.SaveChangesAsync();

            return eventDTO;
        }

        public async Task<bool> DeleteEventAsync(int eventID)
        {
            EventDTO eventDTO = _dbcontext.Event.Find(eventID);
            _dbcontext.Event.Remove(eventDTO);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
