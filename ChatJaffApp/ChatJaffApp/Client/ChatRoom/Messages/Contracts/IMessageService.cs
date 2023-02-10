using ChatJaffApp.Client.ChatRoom.Messages.Models;
using ChatJaffApp.Client.Shared.Models;
using static ChatJaffApp.Client.ChatRoom.Pages.ChatRoom;

namespace ChatJaffApp.Client.ChatRoom.Messages.Contracts
{
    public interface IMessageService
    {
        Task<ServiceResponseViewModel<ReceiveMessageDto>> EditMessage(EditMessageRequest newMessage);
        Task<ServiceResponseViewModel<string>> DeleteMessage(Guid id);

    }
}
