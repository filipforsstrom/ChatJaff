using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.Chat.CreateChat.Models
{
    public class InviteMemberRequest
    {
        [Required]
        [MinLength(3)]
        public string? SearchedUsername { get; set; } = string.Empty;
    }
}
