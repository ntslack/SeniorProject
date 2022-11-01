using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using SeniorProject.Models.Repositories;

namespace SeniorProject.Models.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<List<TaskDTO>> GetTasksAsync(int userID) => _taskRepository.GetTasksAsync(userID);

        public Task<TaskDTO> GetTaskByID(int taskID) => _taskRepository.GetTaskByID(taskID);

        public Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO) => _taskRepository.CreateTaskAsync(taskDTO);

        public Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDTO) => _taskRepository.UpdateTaskAsync(taskDTO);

        public Task<int> DeleteTaskAsync(TaskDTO taskDTO) => _taskRepository.DeleteTaskAsync(taskDTO);
    }
}
