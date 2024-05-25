using Expense_Tracker.DataBase;
using Expense_Tracker.Errors;
using Expense_Tracker.RequestJson;
using Expense_Tracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("Expense-Tracker/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private UserManager<User> userManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Login(RequestJson.LoginRequest authJson)
        {
            var user = await userManager.FindByEmailAsync(authJson.Email);
            var result = await userManager.CheckPasswordAsync(user, authJson.Password);
            if (result)
            {
                return Ok(TokenService.CreateAccesToken(user.Id));
            }
            else
            {

                return BadRequest(new UserNotFoundExc().Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequest authJson)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            bool isValidEmail = Regex.IsMatch(authJson.Email, pattern);
            if (isValidEmail)
            {
                var user = new User() { Email = authJson.Email,UserName= authJson.Username };
                var result = await userManager.CreateAsync(user, authJson.Password);
                
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new InvalidData().Message);
                }

            }
            return BadRequest(new InvalidData().Message);

        }
    }
}
