using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Identity.Models.Contracts;
using JaffChat.Server.Identity.Models;

namespace ChatJaffApp.Server.Identity.Services
{
    public interface IIdentityService
    {
        Task<ILoginResponseDto> LoginAsync(ILoginRequestDto loginRequest);
        Task<ApplicationUser> GetUserFromIdentityDb(string userName);

    }
}
