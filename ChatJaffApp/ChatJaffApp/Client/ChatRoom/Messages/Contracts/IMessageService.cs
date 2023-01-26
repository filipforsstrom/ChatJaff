namespace ChatJaffApp.Client.ChatRoom.Messages.Contracts
{
    public interface IMessageService
    {
        Task SendMessage();
        Task GetChatMessages();

    }
}
