using System.Linq;
using System.Threading.Tasks;
using System;
using XProject.Models;
using Microsoft.EntityFrameworkCore;

namespace XProject.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Project> Projects { get; set; }

        public ValueTask<Project> InsertProjectAsync(Project project) =>
            InsertAsync(project);

        public IQueryable<Project> SelectAllProjects() =>
            SelectAll<Project>();

        public ValueTask<Project> SelectProjectByIdAsync(Guid Id) =>
            SelectByIdAsync<Project>(Id);

        public ValueTask<Project> UpdateProjectAsync(Project project) =>
            UpdateAsync(project);

        public ValueTask<Project> DeleteProjectAsync(Project project) =>
            DeleteAsync(project);
    }
}
