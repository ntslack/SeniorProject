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
                                 reminderTitle = r.reminderTitle,
                                 reminderDescription = r.reminderDescription,
                                 reminderCreationDate = r.reminderCreationDate
                             }).ToListAsync();
            return await reminders;
        }
    }
}
