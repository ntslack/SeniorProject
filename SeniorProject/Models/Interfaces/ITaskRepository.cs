using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskDTO>> GetTasksAsync(int userID);
    }
}
