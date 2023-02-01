using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(Chat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        bool AddMemberToChat(AddMemberToChatDto member);
        Task<Chat> GetChatRoomAsync(Guid chatId);
        Task UpdateChatRoomAsync(Chat chatRoomToUpdate);
    }
}
