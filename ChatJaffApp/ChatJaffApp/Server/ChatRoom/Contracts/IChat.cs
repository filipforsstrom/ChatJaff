using ChatJaffApp.Server.Data.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChat
    {
        Guid Id { get; set; }
        List<Guid> ChatMembersIds { get; set; }
        string Creator { get; set; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }

        ICollection<IMessage> Messages { get; set; }
    }
}
