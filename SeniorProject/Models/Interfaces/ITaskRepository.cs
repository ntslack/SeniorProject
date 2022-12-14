using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskDTO>> GetTasksAsync(int userID);

        Task<TaskDTO> GetTaskByID(int taskID);

        Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO);

        Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDTO);

        Task<bool> DeleteTaskAsync(int taskID);
    }
}
