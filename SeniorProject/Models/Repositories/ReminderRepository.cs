using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        DatabaseContext _dbcontext;
        public ReminderRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<ReminderDTO>> GetRemindersAsync(int userID)
        {
            var reminders = (from r in _dbcontext.Reminder
                             join u in _dbcontext.User
                             on r.userID equals u.userID
                             where r.userID == userID
                             orderby r.reminderCreationDate descending
                             select new ReminderDTO()
                             {
                                 reminderID = r.reminderID,
                                 userID = r.userID,
                                 reminderTitle = r.reminderTitle,
                                 reminderDescription = r.reminderDescription,
                                 reminderCreationDate = r.reminderCreationDate
                             }).ToListAsync();
            return await reminders;
        }

        public async Task<ReminderDTO> GetReminderByID(int reminderID)
        {
            var reminder = await (from r in _dbcontext.Reminder
                              where r.reminderID == reminderID
                              select new ReminderDTO()
                              {
                                  reminderID = r.reminderID,
                                  reminderTitle = r.reminderTitle,
                                  reminderDescription = r.reminderDescription
                              }).SingleOrDefaultAsync();
            return reminder;
        }

        public async Task<ReminderDTO> CreateReminderAsync(ReminderDTO reminderDTO)
        {
            await _dbcontext.AddAsync(reminderDTO);
            await _dbcontext.SaveChangesAsync();

            return reminderDTO;
        }

        public async Task<ReminderDTO> UpdateReminderAsync(ReminderDTO reminderDTO)
        {
            if (reminderDTO == null)
            {
                throw new ArgumentNullException(nameof(reminderDTO));
            }

            _dbcontext.Update(reminderDTO);
            await _dbcontext.SaveChangesAsync();

            return reminderDTO;
        }

        public async Task<bool> DeleteReminderAsync(int reminderID)
        {
            ReminderDTO reminderDTO = _dbcontext.Reminder.Find(reminderID);
            _dbcontext.Reminder.Remove(reminderDTO);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
