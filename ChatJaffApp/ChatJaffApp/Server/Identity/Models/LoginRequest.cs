using System.ComponentModel.DataAnnotations;

namespace JaffChat.Server.Identity.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
