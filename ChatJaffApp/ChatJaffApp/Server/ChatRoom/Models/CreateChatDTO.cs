
namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class CreateChatDTO
    {
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<Guid>? ChatMembersIds { get; set; }
        public Guid CreatorId { get; set; }
    }
}
