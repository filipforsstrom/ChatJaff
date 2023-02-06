using ChatJaffApp.Client.ChatRoom.CreateChat.Contracts;
using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ChatJaffApp.Client.ChatRoom.CreateChat.Services
{
    public class CreateChatService : ICreateChatService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CreateChatService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<CreateChatResponse> CreateChat(CreateChatDto createChatRequest)
        {
            CreateChatResponse chatResponse = new();

            var user = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userId = user.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            createChatRequest.ChatMembersIds.Add(Guid.Parse(userId));

            var response = await _httpClient.PostAsJsonAsync("api/chatroom", createChatRequest);
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
