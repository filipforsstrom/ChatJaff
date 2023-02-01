using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data;
using ChatJaffApp.Server.Data.Contracts;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly JaffDbContext _context;

        public static List<IChat> ChatRooms { get; set; } = new();

        public ChatRoomRepository(JaffDbContext context)
        {

            _context = context;
        }

        public async Task<Guid> CreateChatRoomAsync(IChat chatRoom)
        {
            chatRoom.Id = Guid.NewGuid();
            ChatRooms.Add(chatRoom);
            var createdRoom = ChatRooms.LastOrDefault();
            return createdRoom.Id;
        }
        public IEnumerable<IChat> GetAllChatRooms()
        {
            return ChatRooms;
        }

        public bool AddMemberToChat(AddMemberToChatDto chatMemberData)
        {
            var chatRoom = ChatRooms.FirstOrDefault(c => c.Id == chatMemberData.ChatId);
            chatRoom.ChatMembersIds.Add(chatMemberData.UserId);

            if(!chatRoom.ChatMembersIds.Contains(chatMemberData.UserId))
            {
                return false;
            }

            return true;
        }
    }
}
