using ChatJaffApp.Client.Account.Models;

namespace ChatJaffApp.Client.Account.Contracts
{
    public interface IIdentityService
    {
        Task<RegisterResponse> Register(RegisterForm register);

        Task<RegisterResponse> Login(LoginDto login);
        
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordForm changePassword);

    }
}
