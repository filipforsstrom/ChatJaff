using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Shared.Models;
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

        public async Task<List<ChatMemberViewModel>> GetChatMembers(Guid chatId)
        {
            var response = await _httpClient.GetAsync($"/api/chatroom/{chatId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatMemberViewModel>();
            }

            var chatRoom = await response.Content.ReadFromJsonAsync<GetChatRoomDto>();
            return chatRoom.ChatMembers;
        }

        public async Task<List<ChatRoomsViewModel>> GetMyChats()
        {
            var response = await _httpClient.GetAsync($"/api/chatroom");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatRoomsViewModel>();
            }
            var MyChats = await response.Content.ReadFromJsonAsync<List<ChatRoomsViewModel>>();
            return MyChats;
        }
     
        

        public async Task<GetChatRoomDto> GetChatRoom(Guid chatId)
        {
            var response = await _httpClient.GetAsync($"/api/chatroom/{chatId}");

            if (!response.IsSuccessStatusCode)
            {
                return new GetChatRoomDto();
            }

            var chatRoom = await response.Content.ReadFromJsonAsync<GetChatRoomDto>();
            return chatRoom;
        }

        public async Task<ServiceResponseViewModel<string>> DeleteChatRoom(Guid chatId)
        {
            ServiceResponseViewModel<string> responseViewModel = new();
            var response = await _httpClient.DeleteAsync($"api/chatroom/{chatId}");

            if (!response.IsSuccessStatusCode)
            {
                responseViewModel.Message = "Something went wrong.";
                return responseViewModel;
            }

            responseViewModel.Message = await response.Content.ReadAsStringAsync();
            responseViewModel.Success = true;
            return responseViewModel;

        }
    }
}
