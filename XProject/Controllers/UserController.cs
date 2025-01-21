using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTFulSense.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Brokers.Email;
using XProject.Exceptions.UserException;
using XProject.Models;
using XProject.Services.UserService;

namespace XProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RESTFulController
    {
        private readonly IUserService userService;
        private readonly IEmailBroker emailBroker;

        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPut("deactivate/{userId}")]
        [Authorize(Roles = "admin")] 
        public async Task<IActionResult> DeactivateUser(Guid userId)
        {
            var user = await userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.IsActive = false; 
            await userService.UpdateUserAsync(user);

            return Ok("User has been deactivated successfully");
        }

        [HttpPut("reactivate/{userId}")]
        [Authorize(Roles = "admin")] 
        public async Task<IActionResult> ReactivateUser(Guid userId)
        {
            var user = await userService.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.IsActive = true;
            await userService.UpdateUserAsync(user);

            return Ok("User has been reactivated successfully");
        }


        [HttpGet("get-all-users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersListAsync()
        {
            IQueryable<User> users = 
                this.userService.GetAllUsers();

            if(await users.CountAsync() == 0)
                return NoContent();

            return Ok(await users.ToListAsync());
        }

        [HttpGet("get-user")]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await this.userService.GetUserAsync(id);

                return Ok(user);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("adding-user")]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> CreateUserAsync(User user)
        {
            var newUser = await this.userService.AddUserAsync(user);

            return Ok(newUser);
        }

        [HttpPut("edit-user")]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> EditUserAsync(User user)
        {
            try
            {
                User editingUser = await 
                    this.userService.UpdateUserAsync(user);

                return Ok(editingUser);
            }
            catch (UserNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-user")]
        [Authorize(Roles = "admin")]
        public async ValueTask<IActionResult> DeleteUserAsync(User user)
        {
            try
            {
                User editingUser = await
                    this.userService.DeleteUserAsync(user);

                return Ok(editingUser);
            }
            catch (UserNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class RegisterRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class VerifyCodeDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }

}
