using ChatJaffApp.Client.Chat.CreateChat.Models;
using ChatJaffApp.Client.Chat.Member.Models;

namespace ChatJaffApp.Client.Chat.Member.Contracts
{
    public interface IMemberService
    {
        Task<InviteMemberResponse> AddMemberToChat(InviteMemberDto invMemberDto);
    }
}
