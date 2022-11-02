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
                         join u in _dbcontext.User
                         on t.userID equals u.userID
                         where t.userID == userID
                         orderby t.taskCreationDate descending
                         select new TaskDTO()
                         {
                             taskID = t.taskID,
                             taskValue = t.taskValue,
                             taskCreationDate = t.taskCreationDate,
                             taskIsFavorited = t.taskIsFavorited
                         }).ToListAsync();
            return await tasks;
        }

        public async Task<TaskDTO> GetTaskByID(int taskID)
        {
            var task = await (from t in _dbcontext.Task
                              where t.taskID == taskID
                              select new TaskDTO()
                              {
                                  taskID = t.taskID,
                                  taskValue = t.taskValue
                              }).SingleOrDefaultAsync();
            return task;
        }

        public async Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO)
        {
            await _dbcontext.AddAsync(taskDTO);
            await _dbcontext.SaveChangesAsync();

            return taskDTO;
        }

        public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDTO)
        {
            if (taskDTO == null)
            {
                throw new ArgumentNullException(nameof(taskDTO));
            }

            _dbcontext.Update(taskDTO);
            await _dbcontext.SaveChangesAsync();

            return taskDTO;
        }

        public async Task<int> DeleteTaskAsync(TaskDTO taskDTO)
        {
            _dbcontext.Set<TaskDTO>().Remove(taskDTO);
            await _dbcontext.SaveChangesAsync();

            return 0;
        }

        public async Task<bool> DeleteTaskAsync(int taskID)
        {
            TaskDTO taskDTO = _dbcontext.Task.Find(taskID);
            _dbcontext.Task.Remove(taskDTO);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
