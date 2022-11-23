using SeniorProject.Models;
using SeniorProject.Models.Context;
using SeniorProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        DatabaseContext _dbcontext;
        public UserRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<UserAccount>> GetUsersAsync()
        {
            var users = (from u in _dbcontext.User
                         orderby u.username descending
                         select new UserAccount()
                         {
                             userID = u.userID,
                             username = u.username,
                             firstName = u.firstName,
                             lastName = u.lastName,
                             mobile = u.mobile,
                             isAdmin = u.isAdmin
                         }).ToListAsync();
            return await users;
        }

        public async Task<UserAccount> UpdateUserAsync(UserAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _dbcontext.Update(account);
            await _dbcontext.SaveChangesAsync();

            return account;
        }

        public async Task<bool> DeleteUserAsync(int userID)
        {
            UserAccount account = _dbcontext.User.Find(userID);
            _dbcontext.User.Remove(account);
            await _dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
