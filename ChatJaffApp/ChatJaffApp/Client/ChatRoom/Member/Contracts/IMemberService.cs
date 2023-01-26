using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;

namespace ChatJaffApp.Client.ChatRoom.Member.Contracts
{
    public interface IMemberService
    {
        Task<InviteMemberResponse> AddMemberToChat(InviteMemberDto invMemberDto);
    }
}
