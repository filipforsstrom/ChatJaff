using ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using System.Net.Http.Json;

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
    }
}
