using Microsoft.AspNetCore.Http;
using SeniorProject.Models.DTOs;
using SeniorProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.Services;

namespace SeniorProject.Controllers
{
    [Route("Home/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksAsync(int userID)
        {
            List<TaskDTO>? tasks = await _taskService.GetTasksAsync(userID);
            return Ok(tasks);
        }

        [HttpGet("{taskID}")]
        public async Task<IActionResult> GetTaskByID(int taskID)
        {
            TaskDTO? task = await _taskService.GetTaskByID(taskID);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDTO taskDTO)
        {
            var task = await _taskService.CreateTaskAsync(taskDTO);
            return Ok(task);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync(TaskDTO taskDTO)
        {
            var task = await _taskService.UpdateTaskAsync(taskDTO);
            return Ok(task);
        }

        [HttpDelete("{taskID}")]
        public async Task<IActionResult> DeleteTaskAsync(int taskID)
        {
            bool task = await _taskService.DeleteTaskAsync(taskID);
            return Ok(task);
        }
    }
}
