using System.Linq;
using System.Threading.Tasks;
using System;
using XProject.Models;

namespace XProject.Services.ProjectService
{
    public interface IProjectService
    {
        Task<Project> AddProjectAsync(Project project);
        IQueryable<Project> GetAllProjects();
        Task<Project> GetProjectByTitleAsync(string title);
        Task<Project> UpdateProjectAsync(Project project);
        Task<Project> DeleteProjectAsync(Project project);
    }
}