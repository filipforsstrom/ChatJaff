using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Controllers;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data;
using ChatJaffApp.Server.Data.Contracts;
using ChatJaffApp.Server.Data.Models;
using ChatJaffApp.Server.Hubs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly JaffDbContext _context;

        public MessageRepository(JaffDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddMessageAsync(Message message)
        {
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return message.Id;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteMessage(Guid id)
        {
            try
            {
                var message = await GetMesssage(id);
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

        }

        public async Task<bool> EditMessage(EditMessageDto newMessage)
        {
            try
            {
                var oldMessage = await GetMesssage(newMessage.MessageId);
                oldMessage.Content = newMessage.EditedMessage;

                _context.Update(oldMessage);
                var result = await _context.SaveChangesAsync();

                if(result == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }

        public async Task<Message> GetMesssage(Guid id)
        {

            var message = _context.Messages.Select(m => m).Where(m => m.Id == id).FirstOrDefault();
            if (message == null)
            {
                return new Message();
            }
            return message;
        }

        public async Task<bool> FlagMessage(Guid messageId)
        {
            try
            {
                var oldMessage = await GetMesssage(messageId);
                oldMessage.IsFlagged = true;

                _context.Update(oldMessage);
                var result = await _context.SaveChangesAsync();

                if (result ==null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}