using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.Hubs;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
}