using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChat
    {
        Guid Id { get; set; }
        List<ChatMember> ChatMembers { get; }
        string Creator { get; set; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }
        //List<string> Messages { get; set; }
        void AddMember(Guid userId);
        ICollection<Message> Messages { get; set; }
    }
}
