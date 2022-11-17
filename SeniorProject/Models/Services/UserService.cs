using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using SeniorProject.Models.Interfaces;

namespace SeniorProject.Models.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<UserAccount>> GetUsersAsync() => _userRepository.GetUsersAsync();

        public Task<UserAccount> UpdateUserAsync(UserAccount account) => _userRepository.UpdateUserAsync(account);

        public Task<bool> DeleteUserAsync(int userID) => _userRepository.DeleteUserAsync(userID);
    }
}
