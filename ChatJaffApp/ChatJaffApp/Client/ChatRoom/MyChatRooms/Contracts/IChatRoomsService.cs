using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Member.Models;
using ChatJaffApp.Client.Shared.Models;
using static ChatJaffApp.Client.ChatRoom.MyChatRooms.Services.ChatRoomsService;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts
{
    public interface IChatRoomsService
    {
        Task<List<ChatRoomsViewModel>> GetAllChats();
        Task<List<ChatRoomsViewModel>> GetMyChats();
        Task<GetChatRoomDto> GetChatRoom(Guid chatId);
        Task<ServiceResponseViewModel<string>> DeleteChatRoom(Guid chatId);
        Task<string> RemoveChatMember(Guid chatId, Guid userId);
        Task<ServiceResponseViewModel<ChatMemberResponse>> AddChatMember(AddMemberDto addMemberDto);
    }
}
