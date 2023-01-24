using ChatJaffApp.Client.Chat.CreateChat.Models;

namespace ChatJaffApp.Client.Chat.CreateChat.Contracts
{
    public interface ICreateChatService
    {
        Task<CreateChatResponse> CreateChat(CreateChatDTO createChatRequest);

    }
}
