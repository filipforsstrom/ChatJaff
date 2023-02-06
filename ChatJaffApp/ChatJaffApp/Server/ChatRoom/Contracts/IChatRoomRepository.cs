using ChatJaffApp.Server.ChatRoom.Controllers;
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
        ChatRoomDto ConvertChatToDto(Chat chatRoom);
    }
}
