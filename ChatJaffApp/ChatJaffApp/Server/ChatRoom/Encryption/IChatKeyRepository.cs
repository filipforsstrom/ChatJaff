namespace ChatJaffApp.Server.ChatRoom.Encryption
{
    public interface IChatKeyRepository
    {
        Task AddChatKeyAsync(Guid chatRoomId, string key);
        Task<string> GetChatKeyAsync(Guid chatRoomId);
        Task DeleteChatKey(Guid chatRoomId);
    }
}
