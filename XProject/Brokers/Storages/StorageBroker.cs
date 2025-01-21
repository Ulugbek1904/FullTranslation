using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async ValueTask<T> InsertAsync<T>(T entity) where T : class
        {
            await this.Set<T>().AddAsync(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            return this.Set<T>();
        }

        public async ValueTask<T> SelectByIdAsync<T>(Guid id) where T : class
        {
            return await this.Set<T>().FindAsync(id);
        }

        public async ValueTask<T> UpdateAsync<T>(T entity) where T : class
        {
            this.Set<T>().Update(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        public async ValueTask<T> DeleteAsync<T>(T entity) where T : class
        {
            this.Set<T>().Remove(entity);
            await this.SaveChangesAsync();

            return entity;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.
                configuration.GetConnectionString("DefaultConnectionString");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Localization>()
                .HasOne(l => l.Project)
                .WithMany(p => p.Localizations)
                .HasForeignKey(l => l.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LocalizationValue>()
                .HasOne(lv => lv.Localization)
                .WithMany(l => l.Values)
                .HasForeignKey(lv => lv.LocalizationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Ulug'bek",
                    LastName = "Jumaboyev",
                    Email = "julugbek023@gmail.com",
                    Password = "Qwerty1904",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Role = "admin",
                });
        }
    }
}
