using Blazored.LocalStorage;
using ChatJaffApp.Client.Account.Contracts;
using ChatJaffApp.Client.Account.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ChatJaffApp.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IIdentityService _identityService;
        private CurrentUserDto? _currentUser;

        public CustomAuthStateProvider(IIdentityService identityService)
        {
            _identityService= identityService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<CurrentUserDto> GetCurrentUser()
        {
            if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
            _currentUser = await _identityService.CurrentUserInfo();
            return _currentUser;
        }

        public async Task Logout()
        {
            await _identityService.Logout();
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginDto loginParameters)
        {
            await _identityService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(RegisterForm registerParameters)
        {
            await _identityService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

    }
}