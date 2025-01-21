using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using XProject.Brokers.Storages;
using XProject.Exceptions.UserException;
using XProject.Models;

namespace XProject.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;

        public UserService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            var users = GetAllUsers();
            return Task.FromResult(users.FirstOrDefault(u => u.Email == email));
        }

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
                throw new UserNullException("User can not be null.");

            await this.storageBroker.InsertUserAsync(user);

            return user;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            var existingUser = await 
                this.storageBroker.SelectUserByIdAsync(user.Id);

            if (existingUser == null)
                throw new UserNotFoundException("User does not exist");

            await this.DeleteUserAsync(existingUser);

            return existingUser;

        }

        public IQueryable<User> GetAllUsers()
        {
            var Users = this.storageBroker.SelectAllUsers();
            if(Users.Count() == 0)
            {
                throw new Exception("User list is empty");
            }

            return Users;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await this.
                storageBroker.SelectUserByIdAsync(id);

            return user == null ?
                throw new UserNotFoundException("User not found")
                    : user;
        }

        public async Task<User> GetUserByUsernameAsync(string firstName)
        {
            IQueryable<User> users = GetAllUsers();
            
            var user = await users.FirstOrDefaultAsync(u => u.FirstName == firstName);

            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existingUser = await this.
                storageBroker.SelectUserByIdAsync(user.Id);

            if (existingUser == null)
                throw new UserNotFoundException("User not found");

            await this.storageBroker.UpdateUserAsync(user);

            return user;
        }

        public bool VerifyPassword(string password, string maybePassword)
        {
            try
            {
                if (password == maybePassword)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Password verification failed: {ex.Message}");
            }
        }
    }
}
