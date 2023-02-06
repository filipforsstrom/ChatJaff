using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(Chat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        IEnumerable<IChat> GetMyChatRooms(Guid memberId);
        bool AddMemberToChat(AddMemberToChatDto member);
        Task<Chat> GetChatRoomAsync(Guid chatId);
        Task UpdateChatRoomAsync(Chat chatRoomToUpdate);
        Task<List<Data.Models.Member>> GetChatMembers(Guid chatId);
    }
}
