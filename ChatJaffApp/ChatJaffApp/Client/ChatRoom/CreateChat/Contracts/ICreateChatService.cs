using ChatJaffApp.Client.ChatRoom.CreateChat.Models;

namespace ChatJaffApp.Client.ChatRoom.CreateChat.Contracts
{
    public interface ICreateChatService
    {
        Task<CreateChatResponse> CreateChat(CreateChatDto createChatRequest);

    }
}
