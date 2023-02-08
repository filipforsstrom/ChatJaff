using ChatJaffApp.Client.Shared.Models;

namespace ChatJaffApp.Client.ChatRoom.Messages.Contracts
{
    public interface IMessageService
    {
        Task SendMessage();
        Task GetChatMessages();
        Task<ServiceResponseViewModel<string>> DeleteMessage(Guid id);

    }
}
