using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.Shared.Models;

namespace ChatJaffApp.Client.ChatRoom.Member.Contracts
{
    public interface IMemberService
    {
        Task<ServiceResponseViewModel<ChatMemberResponse>> GetChatMember(InviteMemberDto invMemberDto);
        Task<ServiceResponseViewModel<ChatMemberResponse>> AddChatMember(AddMemberDto invMemberDto);
    }
}
