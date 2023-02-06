using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using static ChatJaffApp.Client.ChatRoom.MyChatRooms.Services.ChatRoomsService;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Models
{
    public class GetChatRoomDto
    {
        public Guid Id { get; set; }
        public List<ChatMemberViewModel>? ChatMembers { get; set; }
        public Guid? CreatorId { get; set; }
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<GetMessageDto>? Messages { get; set; }
    }
}
