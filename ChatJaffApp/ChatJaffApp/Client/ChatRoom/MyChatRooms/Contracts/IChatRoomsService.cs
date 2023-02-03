using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts
{
    public interface IChatRoomsService
    {
        Task<List<ChatRoomsViewModel>> GetAllChats();
        Task<List<ChatRoomsViewModel>> GetMyChats(Guid userId);
        Task<List<ChatMember>> GetChatMembers(Guid chatId);

    }
}
