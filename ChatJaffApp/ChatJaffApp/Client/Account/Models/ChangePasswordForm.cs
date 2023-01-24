using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.Account.Models
{
    public class ChangePasswordForm
    {
        [Required]
        [MinLength(6)]
        public string? OldPassword { get; set; }
        [Required]
        [MinLength(6)]
        public string? NewPassword { get; set; }
    }
}
