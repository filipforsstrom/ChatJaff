using ChatJaffApp.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            ApplicationUser newUser = new()
            {
                Email = request.Email,
                UserName = request.Username,
            };

            try
            {
                var registerResult = await _signInManager.UserManager.CreateAsync(newUser, request.Password);

                if (registerResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }

                return BadRequest(registerResult.Errors);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

        }
    }
}
