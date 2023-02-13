using ChatJaffApp.Client.ChatRoom.Messages.Models;
using ChatJaffApp.Client.Shared.Models;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;
using static ChatJaffApp.Client.ChatRoom.Pages.ChatRoom;

namespace ChatJaffApp.Server.ChatRoom.Contracts;

public interface IMessageRepository
{
    Task<Guid> AddMessageAsync(Message message);
    Task DeleteMessage(Guid id);

    Task<bool> EditMessage(EditMessageDto newMessage);
    Task<Message> GetMesssage(Guid id);
}
