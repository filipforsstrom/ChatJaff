using ChatJaffApp.Server.ChatRoom.Controllers;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public List<ChatMemberDto> ChatMembers { get; set; }
        public Guid? CreatorId { get; set; }
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
