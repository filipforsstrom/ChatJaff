﻿using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.Account.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;

        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordForm changePassword)
        {
            ChangePasswordResponse changePasswordResponse = new();

            ChangePasswordDTO changePasswordDTO = new()
            {
                OldPassword= changePassword.OldPassword,
                NewPassword= changePassword.NewPassword,
            };
            var apiResponse = await _httpClient.PostAsJsonAsync("api/identity/changepassword", changePasswordDTO);

            if (!apiResponse.IsSuccessStatusCode)
            {
                changePasswordResponse.Data = await apiResponse.Content.ReadAsStringAsync();
                return changePasswordResponse;
            }

            changePasswordResponse.Success = true;
            return changePasswordResponse;
        }

        public async Task<RegisterResponse> Register(RegisterForm register)
        {
            RegisterResponse registerResponse = new();

            RegisterDTO registerRequest = new RegisterDTO()
            {
                Email = register.Email,
                Password = register.Password,
                Username = register.Username,
            };

            var response = await _httpClient.PostAsJsonAsync("api/Identity/Register", registerRequest);

            if (!response.IsSuccessStatusCode)
            {
                registerResponse.Data = await response.Content.ReadAsStringAsync();
                return registerResponse;
            }

            registerResponse.Success = true;
            return registerResponse;

        }
    }
}
