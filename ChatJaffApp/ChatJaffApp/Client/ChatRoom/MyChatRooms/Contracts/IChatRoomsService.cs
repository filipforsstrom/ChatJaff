using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts
{
    public interface IChatRoomsService
    {
        Task<List<ChatRoomsViewModel>> GetAllChats();
    }
}
