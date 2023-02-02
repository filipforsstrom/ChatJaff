using ChatJaffApp.Server.ChatRoom.Member.Models;

namespace ChatJaffApp.Server.ChatRoom.Member.Contracts
{
    public interface IMemberRepository
    {
        Task<GetMemberDto> GetMember(string searchedUserName);
    }
}
