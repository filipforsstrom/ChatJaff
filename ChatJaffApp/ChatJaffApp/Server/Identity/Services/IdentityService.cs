using ChatJaffApp.Server.Identity.Models;
using ChatJaffApp.Server.Identity.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace ChatJaffApp.Server.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public IdentityService(SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<ILoginResponseDto> LoginAsync(ILoginRequestDto loginRequest)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                throw new NullReferenceException($"Couldn't find any user with email {loginRequest.Email}");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginRequest.Password, false, false);

            if (!signInResult.Succeeded) throw new AuthenticationException(signInResult.ToString());

            var jwt = await CreateTokenAsync(user);

            return new LoginResponseDto
            {
                Token = jwt

            };

        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = await CreateUserClaimsAsync(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
               _configuration.GetSection("JWTSettings:SecretForKey").Value));
               
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWTSettings:Issuer"],
                _configuration["JWTSettings:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(3000),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private async Task<List<Claim>> CreateUserClaimsAsync(ApplicationUser user)
        {
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            return claims;
        }
    }
}
