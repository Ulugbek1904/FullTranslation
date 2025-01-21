using System.Linq;
using System.Threading.Tasks;
using System;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<User> InsertUserAsync(User user);
        public IQueryable<User> SelectAllUsers();
        public ValueTask<User> SelectUserByIdAsync(Guid Id);
        public ValueTask<User> UpdateUserAsync(User user);
        public ValueTask<User> DeleteUserAsync(User user);
    }
}
