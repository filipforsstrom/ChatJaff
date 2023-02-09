using ChatJaffApp.Server.Data;
using System.Security.Cryptography;

namespace ChatJaffApp.Server.ChatRoom.Encryption
{
    public class ChatKeyRepository : IChatKeyRepository
    {
        private readonly JaffDbContext _context;

        public ChatKeyRepository(JaffDbContext context)
        {
            _context = context;
        }
        public Task AddChatKeyAsync(Guid chatRoomId, string key)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteChatKey(Guid chatRoomId)
        {
            var chatKey = await _context.ChatKeys.FindAsync(chatRoomId);

            if (chatKey != null)
            {
                _context.ChatKeys.Remove(chatKey);
                await _context.SaveChangesAsync();
            } else
            {
                throw new NullReferenceException();
            }

        }

        public async Task<string> GetChatKeyAsync(Guid chatRoomId)
        {
            try
            {
                var chatKey = await _context.ChatKeys.FindAsync(chatRoomId);

                if (chatKey != null)
                {
                    return chatKey.Key;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
