using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.Data;
using ChatJaffApp.Server.Data.Models;
using ChatJaffApp.Server.Hubs;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly JaffDbContext _context;

        public MessageRepository(JaffDbContext context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message)
        {
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}