namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChat
    {
        List<Guid> ChatMembersIds { get; set; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }
    }
}
