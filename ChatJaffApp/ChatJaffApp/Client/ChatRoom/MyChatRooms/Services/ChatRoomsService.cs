using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Contracts;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Member.Models;
using ChatJaffApp.Client.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public async Task<ServiceResponseViewModel<string>> RemoveChatMember(Guid chatId, Guid userId)
        {
            ServiceResponseViewModel<string> responseViewModel = new();
            var response = await _httpClient.DeleteAsync($"api/chatroom/{chatId}/members/{userId}");

            if(!(response.StatusCode == System.Net.HttpStatusCode.NotFound))
            {
                responseViewModel.Message = "Something went wrong";
                responseViewModel.Success = false;
                return responseViewModel;
            }

            responseViewModel.Message = await response.Content.ReadAsStringAsync();
            responseViewModel.Success = true;
            return responseViewModel;
        }

        public async Task<ServiceResponseViewModel<ChatMemberResponse>> AddChatMember(AddMemberDto addMemberDto)
        {
            ServiceResponseViewModel<ChatMemberResponse> addMemberResponse = new();
            var response = await _httpClient.PostAsJsonAsync($"api/chatroom/{addMemberDto.ChatId}/members", addMemberDto.UserId);

            if (!response.IsSuccessStatusCode)
            {
                addMemberResponse.Success = false;
                addMemberResponse.Message = await response.Content.ReadAsStringAsync();
                return addMemberResponse;
            }

            addMemberResponse.Success = true;
            addMemberResponse.Message = await response.Content.ReadAsStringAsync();
            return addMemberResponse;
        }

        public async Task ChangeChatName(string newName, Guid chatroomId)
        {
            UpdateChatroomDto updatedChatroom = new UpdateChatroomDto
            {
                Id= chatroomId,
                Name= newName,
            };

            var updateChatNameResponse = await _httpClient.PutAsJsonAsync($"api/chatroom/{chatroomId}", updatedChatroom);
            if (!updateChatNameResponse.IsSuccessStatusCode)
            {
                var responseMessage = await updateChatNameResponse.Content.ReadAsStringAsync();
                throw new Exception(responseMessage);
            }
        }
    }
}
