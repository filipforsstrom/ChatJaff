using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
}