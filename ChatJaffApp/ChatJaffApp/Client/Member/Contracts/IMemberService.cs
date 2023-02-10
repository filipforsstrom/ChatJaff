using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Member.Models;
using ChatJaffApp.Client.Shared.Models;

namespace ChatJaffApp.Client.Member.Contracts
{
    public interface IMemberService
    {
        Task<ServiceResponseViewModel<ChatMemberResponse>> GetMember(GetMemberDto invMemberDto);
        Task<List<ChatMemberResponse>> GetAllMembers();
    }
}
