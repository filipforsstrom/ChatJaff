using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChat
    {
        Guid Id { get; set; }
        List<ChatMember> ChatMembers { get; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }
        void AddMember(Guid userId);
        ICollection<Message> Messages { get; set; }
        Guid CreatorId { get; set; }
        void RemoveMember(Guid userId);
    }
}
