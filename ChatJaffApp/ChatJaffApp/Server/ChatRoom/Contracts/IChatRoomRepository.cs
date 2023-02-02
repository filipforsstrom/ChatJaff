using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(IChat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        IEnumerable<IChat> GetMyChatRooms(Guid memberId);
        bool AddMemberToChat(AddMemberToChatDto member);
    }
}
