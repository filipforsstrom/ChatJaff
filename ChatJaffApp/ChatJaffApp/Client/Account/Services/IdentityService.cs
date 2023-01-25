using Blazored.LocalStorage;
using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Models;
using ChatJaffApp.Client.Shared.Models;
using ChatJaffApp.Client.Shared.Models.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.Account.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public IdentityService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
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

        public async Task<DeleteIdentityResponse> DeleteIdentity(string identityId)
        {
            DeleteIdentityResponse deleteResponse = new();
            Guid id = Guid.NewGuid();
            var apiResponse = await _httpClient.DeleteAsync($"api/identity/{id}");

            if (apiResponse.IsSuccessStatusCode)
            {
                deleteResponse.Success = true;
            } else
            {
                deleteResponse.Data = await apiResponse.Content.ReadAsStringAsync();
            }

            return deleteResponse;
        }

        public async Task<IServiceResponseViewModel<RegisterResponse>> Login(LoginDto login)
        {
            ServiceResponseViewModel<RegisterResponse> responseViewModel = new();
            var httpResponse = await _httpClient.PostAsJsonAsync("api/identity/login", login);
            if (httpResponse.IsSuccessStatusCode)
            {
                var token = await httpResponse.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("token", token);
                var authState = await _authStateProvider.GetAuthenticationStateAsync();

                responseViewModel.Success = true;
                return responseViewModel;
            }

            responseViewModel.Message = await httpResponse.Content.ReadAsStringAsync();
            return responseViewModel;
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
