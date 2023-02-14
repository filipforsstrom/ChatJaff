using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.Account.Models
{
    public class RegisterForm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", 
            ErrorMessage = "Password must meet requirements: must have at least one non alphanumeric character, must have at least one digit ('0'-'9'), must have at least one uppercase ('A'-'Z'), must have at least one lowercase ('a'-'z').")]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public string? ValidatePassword { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MinLength(10)]
        [MaxLength(12)]
        public string? PhoneNumber { get; set; }

        public bool ConfirmUserTerms { get; set; } = false;
    }
}
