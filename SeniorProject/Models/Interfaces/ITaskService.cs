using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetTasksAsync(int userID);

        Task<TaskDTO> GetTaskByID(int taskID);

        Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO);

        Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDTO);

        Task<int> DeleteTaskAsync(TaskDTO taskDTO);
    }
}
