using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System.Linq;
using System.Threading.Tasks;
using System;
using XProject.Models;
using XProject.Services.ProjectService;
using Microsoft.EntityFrameworkCore;

namespace XProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : RESTFulController
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("get-all-projects")]
        [Authorize]
        public IActionResult GetProjectsListAsync()
        {
            IQueryable<Project> projects =
                this.projectService.GetAllProjects();

            if(projects == null) 
                return NoContent();

            return Ok(projects);
        }

        [HttpGet("get-project")]
        [Authorize]
        public async ValueTask<IActionResult> GetProjectByTitleAsync(string title)
        {
            try
            {
                var project = await 
                    this.projectService.GetProjectByTitleAsync(title);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("adding-project")]
        [Authorize]
        public async ValueTask<IActionResult> CreateProjectAsync(Project project)
        {
            var newProject = await this.projectService.AddProjectAsync(project);

            return Ok(newProject);
        }

        [HttpPut("edit-project")]
        [Authorize]
        public async ValueTask<IActionResult> EditProjectAsync(Project project)
        {
            try
            {
                Project editingProject = await
                    this.projectService.UpdateProjectAsync(project);

                return Ok(editingProject);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete-project")]
        [Authorize]
        public async ValueTask<IActionResult> DeleteProjectAsync(Project project)
        {
            try
            {
                Project editingProject = await
                    this.projectService.DeleteProjectAsync(project);

                return Ok(editingProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
