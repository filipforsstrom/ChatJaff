namespace ChatJaffApp.Server.Chat.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(IChatRoom chatRoom);
    }
}
