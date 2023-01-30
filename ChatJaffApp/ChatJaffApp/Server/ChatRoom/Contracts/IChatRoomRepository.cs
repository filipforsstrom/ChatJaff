using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(IChat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        bool AddMemberToChat(AddMemberToChatDto member);
    }
}
