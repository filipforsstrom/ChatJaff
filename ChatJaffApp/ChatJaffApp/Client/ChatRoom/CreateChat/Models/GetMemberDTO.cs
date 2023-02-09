using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.ChatRoom.CreateChat.Models
{
    public class GetMemberDto
    {
        [Required(ErrorMessage = "Search field must not be empty.")]
        [MinLength(2, ErrorMessage = "Username must be longer than 1 character")]
        public string? SearchedUsername { get; set; } = string.Empty;
    }
}
