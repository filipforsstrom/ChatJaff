using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Services
{
    public class ChatRoomsService : IChatRoomsService
    {
        private readonly HttpClient _httpClient;

        public ChatRoomsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatRoomsViewModel>> GetAllChats()
        {
            var response = await _httpClient.GetAsync("/api/chatroom/getallchats");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatRoomsViewModel>();
            }
            var chatRoomList = await response.Content.ReadFromJsonAsync<List<ChatRoomsViewModel>>();
            return chatRoomList;
        }

        public async Task<List<ChatMember>> GetChatMembers(Guid chatId)
        {
            var response = await _httpClient.GetAsync($"/api/chatroom/{chatId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatMember>();
            }

            var chatMembers = await response.Content.ReadFromJsonAsync<List<ChatMember>>();
            return chatMembers;
        }

        public async Task<List<ChatRoomsViewModel>> GetMyChats(Guid memberId)
        {
            var response = await _httpClient.GetAsync($"/api/chatroom/getmychats/{memberId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatRoomsViewModel>();
            }
            var MyChats = await response.Content.ReadFromJsonAsync<List<ChatRoomsViewModel>>();
            return MyChats;
        }
    }
}
