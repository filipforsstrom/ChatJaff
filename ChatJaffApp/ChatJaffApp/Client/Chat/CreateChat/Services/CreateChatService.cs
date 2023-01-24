using ChatJaffApp.Client.Chat.CreateChat.Contracts;
using ChatJaffApp.Client.Chat.CreateChat.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.Chat.CreateChat.Services
{
    public class CreateChatService : ICreateChatService
    {
        private readonly HttpClient _httpClient;

        public CreateChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreateChatResponse> CreateChat(CreateChatDTO createChatRequest)
        {
            CreateChatResponse chatResponse = new();

            var response = await _httpClient.PostAsJsonAsync<CreateChatDTO>("api/chatroom/createchat", createChatRequest);
            if (!response.IsSuccessStatusCode)
            {
                chatResponse.Data = "Something went wrong";
                chatResponse.Success = false; 
                return chatResponse;
            }

            chatResponse.Success = true;
            return chatResponse;
        }
    }
}
