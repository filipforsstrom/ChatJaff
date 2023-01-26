using ChatJaffApp.Client.ChatRoom.CreateChat.Contracts;
using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.ChatRoom.CreateChat.Services
{
    public class CreateChatService : ICreateChatService
    {
        private readonly HttpClient _httpClient;

        public CreateChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CreateChatResponse> CreateChat(CreateChatDto createChatRequest)
        {
            CreateChatResponse chatResponse = new();

            var response = await _httpClient.PostAsJsonAsync<CreateChatDto>("api/chatroom/createchat", createChatRequest);
            if (!response.IsSuccessStatusCode)
            {
                chatResponse.Success = false; 
                return chatResponse;
            }

            chatResponse.Success = true;
            chatResponse.Data = await response.Content.ReadFromJsonAsync<Guid>();
            return chatResponse;
        }
    }
}
