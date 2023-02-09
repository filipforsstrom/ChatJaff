

using ChatJaffApp.Server.ChatRoom.Member.Models;

namespace ChatJaffApp.Server.ChatRoom.Member.Contracts
{
    public interface IMemberRepository
    {
        Task<GetMemberDto> GetMember(string searchedUserName);
        IEnumerable<ChatJaffApp.Server.Data.Models.Member> GetAllMembers();
        Task AddMemberToDb(Guid userId, string userName);
    }
}
