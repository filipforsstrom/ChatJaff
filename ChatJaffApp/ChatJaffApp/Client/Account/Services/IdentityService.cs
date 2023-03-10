using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Models;
using ChatJaffApp.Client.Shared.Models;
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
                OldPassword = changePassword.OldPassword,
                NewPassword = changePassword.NewPassword,
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

        public async Task<DeleteIdentityResponseDto> DeleteIdentity(string identityId)
        {
            DeleteIdentityResponseDto deleteResponse = new();

            var guidId = Guid.Parse(identityId);

            var apiResponse = await _httpClient.DeleteAsync($"api/identity/{guidId}");

            if (apiResponse.IsSuccessStatusCode)
            {
                deleteResponse.Success = true;
            }
            else
            {
                deleteResponse.Data = await apiResponse.Content.ReadAsStringAsync();
            }

            return deleteResponse;
        }

        public async Task Login(LoginDto login)
        {
            var loginResponse = await _httpClient.PostAsJsonAsync("api/identity/login", login);
            if (loginResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await loginResponse.Content.ReadAsStringAsync());
            }
            loginResponse.EnsureSuccessStatusCode();
        }

        public async Task<RegisterResponse> Register(RegisterForm register)
        {
            RegisterResponse registerResponse = new();

            RegisterDTO registerRequest = new RegisterDTO()
            {
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                Password = register.Password,
                Username = register.Username,
                AgreedToUserTerms = register.ConfirmUserTerms,
            };

            var response = await _httpClient.PostAsJsonAsync("api/Identity/Register", registerRequest);

            if (!response.IsSuccessStatusCode)
            {
                registerResponse.Data = await response.Content.ReadAsStringAsync();
                throw new Exception(registerResponse.Data);
            }

            registerResponse.Success = true;
            return registerResponse;

        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/identity/logout", null);
            result.EnsureSuccessStatusCode();

        }

        public async Task<CurrentUserDto> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUserDto>("api/identity/currentuserinfo");
            return result;
        }

        public async Task<ServiceResponseViewModel<string>> ChangeUserName(Guid userId, string userName)
        {
            ServiceResponseViewModel<string> responseViewModel = new();
            var response = await _httpClient.PostAsJsonAsync($"api/identity/{userId}/changeusername", userName);

            if (!response.IsSuccessStatusCode)
            {
                responseViewModel.Success = false;
                responseViewModel.Message = "Failed to change username.";
                return responseViewModel;
            }

            responseViewModel.Success = true;
            responseViewModel.Message = "Username change success!";
            return responseViewModel;
        }
    }
}
