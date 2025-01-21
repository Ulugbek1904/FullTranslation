using System.Linq;
using System.Threading.Tasks;
using System;
using XProject.Models;
using Microsoft.EntityFrameworkCore;

namespace XProject.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<User> Users { get; set; }
        public ValueTask<User> InsertUserAsync(User user) =>
            InsertAsync(user);

        public IQueryable<User> SelectAllUsers() =>
            SelectAll<User>();

        public ValueTask<User> SelectUserByIdAsync(Guid Id) =>
            SelectByIdAsync<User>(Id);

        public ValueTask<User> UpdateUserAsync(User user) =>
            UpdateAsync(user);

        public ValueTask<User> DeleteUserAsync(User user) =>
            DeleteAsync(user);
    }
}
