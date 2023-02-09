using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Shared.Models;

namespace ChatJaffApp.Client.ChatRoom.Member.Contracts
{
    public interface IMemberService
    {
        Task<ServiceResponseViewModel<ChatMemberResponse>> GetMember(GetMemberDto invMemberDto);
        Task<List<ChatMemberResponse>> GetAllMembers();
        Task<ServiceResponseViewModel<ChatMemberResponse>> AddChatMember(AddMemberDto invMemberDto);
    }
}
