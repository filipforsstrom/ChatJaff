using ChatJaffApp.Client.Account.Models;
using ChatJaffApp.Client.Shared.Models;
using ChatJaffApp.Client.Shared.Models.Contracts;

namespace ChatJaffApp.Client.Account.Contracts
{
    public interface IIdentityService
    {
        Task<RegisterResponse> Register(RegisterForm register);

        Task Login(LoginDto login);
        
        Task Logout();

        Task<CurrentUserDto> CurrentUserInfo(); 

        Task<ChangePasswordResponse> ChangePassword(ChangePasswordForm changePassword);
        Task<DeleteIdentityResponseDto> DeleteIdentity(string identityId);
        Task<ServiceResponseViewModel<string>> ChangeUserName(Guid userId, string userName);
    }
}
