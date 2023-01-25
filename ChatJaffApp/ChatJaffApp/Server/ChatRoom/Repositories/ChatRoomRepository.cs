using ChatJaffApp.Server.ChatRoom.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        public static List<IChat> ChatRooms { get; set; }
        public ChatRoomRepository()
        {
            ChatRooms = new();
        }

        public async Task<Guid> CreateChatRoomAsync(IChat chatRoom)
        {
            ChatRooms.Add(chatRoom);
            return Guid.NewGuid();
        }
    }
}
