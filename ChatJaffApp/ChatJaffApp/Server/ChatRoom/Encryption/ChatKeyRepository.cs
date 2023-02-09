using ChatJaffApp.Client.ChatRoom.Pages;
using ChatJaffApp.Server.Data;
using ChatJaffApp.Server.Data.Models;
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
            try
            {
                ChatKey chatKey = new()
                {
                    ChatRoomId = chatRoomId,
                    Key = key
                };

                _context.ChatKeys.Add(chatKey);
                var result = _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            
           

            return Task.CompletedTask;
        }

        public Task DeleteChatKey(Guid chatRoomId)
        {
            throw new NotImplementedException();
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
