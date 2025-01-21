using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Brokers.Storages;
using XProject.Models;

namespace XProject.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IStorageBroker storageBroker;

        public ProjectService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public async Task<Project> AddProjectAsync(Project project)
        {
            await this.storageBroker.InsertProjectAsync(project);

            return project;
        }

        public async Task<Project> DeleteProjectAsync(Project project)
        {
            await this.storageBroker.DeleteProjectAsync(project);

            return project;
        }

        public IQueryable<Project> GetAllProjects()
        {
            var Projects = this.storageBroker.SelectAllProjects();
            if(Projects.Count() == 0)
            {
                throw new Exception("Project list is empty");
            }

            return Projects;
        }

        public Task<Project> GetProjectByTitleAsync(string title)
        {
            var Projects = this.GetAllProjects();

            return Task.FromResult(Projects.FirstOrDefault(p => p.Title == title));
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            await this.UpdateProjectAsync(project);

            return project;
        }
    }
}
