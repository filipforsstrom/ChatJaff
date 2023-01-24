using ChatJaffApp.Server.Identity.Models.Contracts;

namespace ChatJaffApp.Server.Identity.Models
{
    public class LoginResponseDto : ILoginResponseDto
    {
        public string? Token { get; set; }

    }
}
