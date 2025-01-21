using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RESTFulSense.Controllers;
using System.Threading.Tasks;
using XProject.Brokers.Email;
using XProject.Brokers.Tokens;
using XProject.Models.Requests;
using XProject.Services.UserService;
using XProject.Services.ValidationServices;

namespace XProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : RESTFulController
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly ITokenBroker tokenBroker;
        private readonly IEmailBroker emailBroker;
        private readonly IValidationService validationService;

        public AuthController(
            IConfiguration configuration,
            IUserService userService,
            ITokenBroker tokenBroker,
            IEmailBroker emailBroker,
            IValidationService validationService)
        {
            this.configuration = configuration;
            this.userService = userService;
            this.tokenBroker = tokenBroker;
            this.emailBroker = emailBroker;
            this.validationService = validationService;
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userEmail = User.Identity.Name;

            if (request.NewPassword != request.ConfirmNewPassword)
                return BadRequest("New password and confirm password do not match");

            var user = await this.userService.GetUserByEmailAsync(userEmail);

            if (user == null)
                return BadRequest("User not found");

            if (user.Password == request.CurrentPassword)
            {
                user.Password = request.NewPassword;
                await this.userService.UpdateUserAsync(user);

                return Ok("Password changed successfully");
            }

            return BadRequest("Current Password is incorrect");
        }

        [HttpPut("edit-profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileRequest profileDto)
        {
            var userEmail = User.Identity.Name;
            var user = await this.userService.GetUserByEmailAsync(userEmail);

            if (user == null)
                return BadRequest("User Not found");

            user.FirstName = profileDto.FirstName;
            user.LastName = profileDto.LastName;
            user.Email = profileDto.Email;
            await this.userService.UpdateUserAsync(user);

            return Ok("Successfully changed");
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(
            string email,
            string password)
        {
            var user = await this.userService.GetUserByEmailAsync(email);

            if (user == null)
                return BadRequest("Firstly, you need to sign up");

            if (!user.IsActive)
                return Unauthorized("Your account is not active");

            var truePassword = user.Password;
            if (!this.userService.VerifyPassword(truePassword, password))
                return Unauthorized("Password is incorrect");

            var token = this.tokenBroker.GenerateAccessToken(user);

            return Ok(token);
        }


        [HttpGet("getme")]
        public async ValueTask<IActionResult> GetMe(string accessToken)
        {
            bool isActiveToken = this.tokenBroker.ValidateToken(accessToken);

            if (!isActiveToken)
                return Unauthorized("Token is expired.");

            var userEmail = this.tokenBroker.GetEmailFromToken(accessToken);

            if (userEmail == null)
                return Unauthorized("Foydalanuvchi identifikatsiyasi o'chirilgan.");

            var user = await this.userService.GetUserByEmailAsync(userEmail);  

            if (user == null)
                return NotFound("Foydalanuvchi topilmadi.");

            var result = new
            {
                user.Id,
                user.FirstName,
                user.Email,
                user.Role,
                user.LastName,
                user.CreatedAt,
                user.IsActive,
                user.Projects,
                user.Password
            };

            return Ok(result);
        }


        //[HttpPost("register/send-code")]
        //public async ValueTask<IActionResult> SendVerificationCodeAsync(RegisterRequestDto registerRequest)
        //{
        //    try
        //    { 
        //        this.validationService.ValidateEmail(registerRequest.Email);
        //        this.validationService.ValidatePassword(registerRequest.Password);

        //        if (registerRequest.Password != registerRequest.ConfirmPassword)
        //            return BadRequest("Password and ConfirmPassword do not match.");

        //        var existingUser = await this.userService.GetUserByEmailAsync(registerRequest.Email);
        //        if (existingUser != null)
        //            return Conflict("Email is already registered. Please log in.");

        //        var verificationCode = new Random().Next(100000, 999999).ToString();

        //        var verification = new VerificationCode
        //        {
        //            Email = registerRequest.Email,
        //            Code = verificationCode,
        //            ExpiredDate = DateTime.UtcNow.AddMinutes(30),
        //            FirstName = registerRequest.FirstName,
        //            LastName = registerRequest.LastName,
        //            Password = registerRequest.Password
        //        };

        //        var existingVerification = await this.storageBroker.GetAllCodesAsync()
        //            .FirstOrDefaultAsync(v => v.Email == registerRequest.Email);

        //        if (existingVerification != null)
        //        {
        //            existingVerification.Code = verificationCode;
        //            existingVerification.ExpiredDate = verification.ExpiredDate;
        //            existingVerification.FirstName = registerRequest.FirstName;
        //            existingVerification.LastName = registerRequest.LastName;
        //            existingVerification.Password = registerRequest.Password;
        //        }
        //        else
        //        {
        //            await this.storageBroker.InsertCodeAsync(verification);
        //        }

        //        await this.emailBroker.SendEmailAsync(
        //            registerRequest.Email,
        //            "Verification Code",
        //            $"Your verification code is: {verificationCode}");

        //        return Ok("Verification code sent to your email.");
        //    }
        //    catch (InvalidEmailException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (InvalidPasswordFormatException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        //[HttpPost("register/verify")]
        //public async ValueTask<IActionResult> VerifyAndRegisterAsync(VerifyCodeDto verifyCodeDto)
        //{
        //    var verification = await this.storageBroker.GetAllCodesAsync()
        //        .FirstOrDefaultAsync(v => v.Email == verifyCodeDto.Email);

        //    if (verification == null)
        //        return BadRequest("Verification code has expired or is invalid.");

        //    if (verification.ExpiredDate < DateTime.UtcNow)
        //    {
        //        await this.storageBroker.DeleteCodeAsync(verification);
        //        return BadRequest("Verification code has expired.");
        //    }

        //    if (verification.Code != verifyCodeDto.Code)
        //        return BadRequest("Invalid verification code.");


        //    var newUser = new User
        //    {
        //        FirstName = verification.FirstName,
        //        LastName = verification.LastName,
        //        Email = verification.Email,
        //        Password = verification.Password,
        //        Role = "User"
        //    };

        //    await this.userService.AddUserAsync(newUser);
        //    await this.storageBroker.DeleteCodeAsync(verification);

        //    return Ok("User registered successfully.");
        //}
    }
}
