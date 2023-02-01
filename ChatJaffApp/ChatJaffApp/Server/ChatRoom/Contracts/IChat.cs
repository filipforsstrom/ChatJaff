namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChat
    {
        Guid Id { get; set; }
        List<ChatJaffApp.Server.ChatRoom.Models.ChatMember> ChatMembersIds { get; }
        string Creator { get; set; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }
        //List<string> Messages { get; set; }
        void AddMember(Guid userId);
    }
}
