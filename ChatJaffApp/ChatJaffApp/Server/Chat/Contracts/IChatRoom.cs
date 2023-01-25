namespace ChatJaffApp.Server.Chat.Contracts
{
    public interface IChatRoom
    {
        List<Guid> ChatMembersIds { get; set; }
        bool Encrypted { get; set; }
        string ChatName { get; set; }
    }
}
