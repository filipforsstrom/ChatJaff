using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using static ChatJaffApp.Client.ChatRoom.MyChatRooms.Services.ChatRoomsService;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts
{
    public interface IChatRoomsService
    {
        Task<List<ChatRoomsViewModel>> GetAllChats();
        Task<List<ChatRoomsViewModel>> GetMyChats(Guid userId);
        Task<List<ChatMemberViewModel>> GetChatMembers(Guid chatId);
        Task<GetChatRoomDto> GetChatRoom(Guid chatId);

    }
}
