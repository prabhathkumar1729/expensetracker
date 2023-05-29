using BlazorWalletTrackerBL.Models;
using BlazorWalletTrackerBE.Models;

using BlazorWalletTrackerBL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWalletTrackerBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices) 
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserBL user)
        {
            var (IsUserRegistered, Message) = await _userServices.RegisterNewUser(user);
            if(IsUserRegistered)
            {
                return Ok(Message);
            }
            ModelState.AddModelError("Email",Message);
            return BadRequest(ModelState);
        }

        [HttpGet("unique-user-email")]
        public IActionResult IsEmailUnique(string email)
        {
            return Ok(_userServices.CheckUserUniqueEmail(email));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserModel loginPayload)
        {
            var (IsLoginSuccess, TokenResponse) = await _userServices.LoginUser(loginPayload);
            if (IsLoginSuccess)
            {
                return Ok(TokenResponse);
            }

            ModelState.AddModelError("LoginError", "Invalid email or password");
            return BadRequest(ModelState);
        }

        [HttpGet("getUserDetails/{id}")]
        public async Task<IActionResult> GetUser(int id) 
        {
            return Ok(new User { Id = 101, Email = "tester1@abc.com", Name = "Tester 1" });
        }

        [HttpGet("getUserSecurityQuestion/{email}")]
        public async Task<IActionResult> GetUserSecurityQuestion(string email)
        {
            string question = await _userServices.GetUserSecurityQuestion(email);
            if (!String.IsNullOrEmpty(question)) 
            {
                return Ok(await _userServices.GetUserSecurityQuestion(email));
            }
            return BadRequest("User not found with specified email");
        }

        [HttpPost("isUserSecurityAnswerValid")]
        public async Task<IActionResult> IsUserSecurityAnswerValid(UserSecurityAnswerModel userSecurityAnswerModel)
        {
            return Ok(await _userServices.IsUserSecurityAnswerValid(userSecurityAnswerModel.Email, userSecurityAnswerModel.SecurityAnswer));
        }

        [HttpPost("changeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(LoginUserModel changeUserPasswordModel)
        {
            return Ok(await _userServices.ChangeUserPassword(changeUserPasswordModel.Email, changeUserPasswordModel.Password));
        }
    }
}
