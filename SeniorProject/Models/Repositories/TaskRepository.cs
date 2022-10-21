using SeniorProject.Models.DTOs;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        DatabaseContext _dbcontext;
        public TaskRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<TaskDTO>> GetTasksAsync(int userID)
        {
            var tasks = (from t in _dbcontext.Task
                         select new TaskDTO()
                         {
                             taskID = t.taskID,
                             taskValue = t.taskValue,
                             taskCreationDate = t.taskCreationDate,
                             taskIsFavorited = t.taskIsFavorited
                         }).ToListAsync();
            return await tasks;
        }
    }
}
