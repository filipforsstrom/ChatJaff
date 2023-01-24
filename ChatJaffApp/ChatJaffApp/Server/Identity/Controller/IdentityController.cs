using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Identity.Models.Contracts;
using ChatJaffApp.Server.Identity.Services;
using JaffChat.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Principal;

namespace ChatJaffApp.Server.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identityService;

        public IdentityController(SignInManager<ApplicationUser> signInManager, IIdentityService identityService)
        {
            _signInManager = signInManager;
            _identityService = identityService;
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
        }
    }
}
