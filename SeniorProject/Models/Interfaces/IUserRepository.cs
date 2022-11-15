using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorProject.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserAccount>> GetUsersAsync();

        //Task<UserAccount> GetUserByID(int userID);

        Task<UserAccount> UpdateUserAsync(UserAccount account);

        Task<bool> DeleteUserAsync(int userID);
    }
}
