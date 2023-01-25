using ChatJaffApp.Server.Chat.Contracts;

namespace ChatJaffApp.Server.Chat.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        public static List<IChatRoom> ChatRooms { get; set; }
        public ChatRoomRepository()
        {
            ChatRooms = new();
        }

        public async Task<Guid> CreateChatRoomAsync(IChatRoom chatRoom)
        {
            ChatRooms.Add(chatRoom);
            return Guid.NewGuid();
        }
    }
}
