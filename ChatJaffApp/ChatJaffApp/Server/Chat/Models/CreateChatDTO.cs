using ChatJaffApp.Client.Chat.CreateChat.Models;

namespace ChatJaffApp.Server.Chat.Models
{
    public class CreateChatDTO
    {
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<Guid>? ChatMembersIds { get; set; }
    }
}
