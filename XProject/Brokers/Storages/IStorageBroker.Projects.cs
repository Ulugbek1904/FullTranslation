using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<Project> InsertProjectAsync(Project project);
        public IQueryable<Project> SelectAllProjects();
        public ValueTask<Project> SelectProjectByIdAsync(Guid Id);
        public ValueTask<Project> UpdateProjectAsync(Project project);
        public ValueTask<Project> DeleteProjectAsync(Project project);
    }
}
