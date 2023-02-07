using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Identity.Services;
using JaffChat.Server.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Diagnostics;
using ChatJaffApp.Server.ChatRoom.Member.Contracts;
using ChatJaffApp.Server.ChatRoom.Member.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace ChatJaffApp.Server.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public IdentityController(SignInManager<ApplicationUser> signInManager, IIdentityService identityService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IMemberRepository memberRepository)
        {
            _signInManager = signInManager;
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var newUser = _mapper.Map<ApplicationUser>(request);

            try
            {
                var registerResult = await _signInManager.UserManager.CreateAsync(newUser, request.Password);

                if (registerResult.Succeeded)
                {
                    var user = await _identityService.GetUserFromIdentityDb(newUser.Email);
                    await _memberRepository.AddMemberToDb(Guid.Parse(newUser.Id), newUser.UserName);
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
                var userInDb = await _signInManager.UserManager.FindByEmailAsync(user.Email);

                if (userInDb == null) return BadRequest("Invalid Credentials");

                if (userInDb.IsBanned) return BadRequest("User is banned");

                var signInResult = await _signInManager.CheckPasswordSignInAsync(userInDb, user.Password, false);

                if (!signInResult.Succeeded) return BadRequest("Invalid Credentials");

                await _signInManager.SignInAsync(userInDb, new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                    IsPersistent = true
                });

                return Ok();
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

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var userInContext = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (userInContext == null) return Unauthorized();

            await Logout();

            await _signInManager.SignInAsync(userInContext, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1),
                IsPersistent = true
            });

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CurrentUserInfo()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new CurrentUserDto
                {
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    UserName = User.Identity.Name,
                    Claims = User.Claims
                .ToDictionary(c => c.Type, c => c.Value)
                });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdentity(Guid id)
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                if (userId == id.ToString())
                {
                    var userToDelete = await _signInManager.UserManager.FindByIdAsync(userId);
                    if (userToDelete != null)
                    {
                        await _signInManager.UserManager.DeleteAsync(userToDelete);

                        return NoContent();
                    }

                    return BadRequest("Server error");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("kill")]
        public async Task Kill()
        {
            Environment.Exit(0);
        }
    }
}
