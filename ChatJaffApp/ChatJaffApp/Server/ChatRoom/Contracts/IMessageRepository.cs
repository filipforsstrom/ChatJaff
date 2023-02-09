using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Contracts;

public interface IMessageRepository
{
    Task<Guid> AddMessageAsync(Message message);
    Task DeleteMessage(Guid id);
}