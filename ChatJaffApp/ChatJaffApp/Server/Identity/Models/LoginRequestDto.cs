using ChatJaffApp.Server.Identity.Models.Contracts;
using System.ComponentModel.DataAnnotations;

namespace JaffChat.Server.Identity.Models
{
    public class LoginRequestDto:ILoginRequestDto
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

    }
}
