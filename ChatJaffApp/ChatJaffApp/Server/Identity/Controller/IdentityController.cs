using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Identity.Services;
using JaffChat.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace ChatJaffApp.Server.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IdentityController(SignInManager<ApplicationUser> signInManager, IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto user)
        {
            try
            {
                var loginResponse = await _identityService.LoginAsync(user);

                HttpContext.Response.Headers.Add("AuthToken", loginResponse.Token); // for RestClient in vscode
                return Ok(loginResponse);
            }
            catch (AuthenticationException exception)
            {
                return Unauthorized(exception.Message);
            }
            catch (NullReferenceException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (string.Equals(request.OldPassword, request.NewPassword))
            {
                return BadRequest("The new password can't be the old password.");
            }

            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var exampleUser = await _signInManager.UserManager.FindByIdAsync(userId);

            if (exampleUser == null)
            {
                return BadRequest("User not found.");
            }

            var response = await _signInManager.UserManager.ChangePasswordAsync(exampleUser, request.OldPassword, request.NewPassword);

            if (response.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
