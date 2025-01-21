using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> AddUserAsync(User user);
        IQueryable<User> GetAllUsers();
        Task<User> GetUserAsync(Guid id);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string firstName);
        bool VerifyPassword(string password, string maybePassword);
    }
}