﻿using ChatJaffApp.Client.Account.Models;
using ChatJaffApp.Client.Shared.Models.Contracts;

namespace ChatJaffApp.Client.Account.Contracts
{
    public interface IIdentityService
    {
        Task<RegisterResponse> Register(RegisterForm register);

        Task<IServiceResponseViewModel<RegisterResponse>> Login(LoginDto login);
        
        Task<RegisterResponse> Logout();


        Task<ChangePasswordResponse> ChangePassword(ChangePasswordForm changePassword);


    }
}
