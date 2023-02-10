using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.Account.Models
{
    public class RegisterForm
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public string? ValidatePassword { get; set; }

        public bool ConfirmUserTerms { get; set; }=false;
    }
}
