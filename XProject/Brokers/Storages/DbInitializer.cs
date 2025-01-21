using System;
using System.Linq;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    //public static class DbInitializer
    //{
    //    public static async void Initialize(IStorageBroker storageBroker)
    //    {
    //        if (!storageBroker.SelectAll<User>().Any())
    //        {
    //            var superadmin = new User
    //            {
    //                Id = Guid.NewGuid(),
    //                FirstName = "Super",
    //                LastName = "Admin",
    //                Email = "julugbek023@gmail.com",
    //                Password = "Qwerty1904",
    //                IsActive = true,
    //                CreatedAt = DateTime.UtcNow,
    //                Role = "admin"
    //            };

    //            await storageBroker.InsertAsync(superadmin);
    //        }
    //    }
    //}
}
