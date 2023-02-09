using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(Chat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        IEnumerable<IChat> GetMyChatRooms(Guid memberId);
        Task<Chat> GetChatRoomAsync(Guid chatId);
        Task UpdateChatRoomAsync(Chat chatRoomToUpdate);
        Task<bool> DeleteChatRoom(Chat chatRoom);
        ChatRoomDto ConvertChatToDto(Chat chatRoom);
        Task<IEnumerable<ChatMember>> GetChatRoomMembersAsync(Guid chatId);
    }
}
