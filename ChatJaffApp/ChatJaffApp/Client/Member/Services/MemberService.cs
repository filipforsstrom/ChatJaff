using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.MyChatRooms.Models;
using ChatJaffApp.Client.Member.Contracts;
using ChatJaffApp.Client.Member.Models;
using ChatJaffApp.Client.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.Member.Services
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient _httpClient;

        public MemberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ChatMemberResponse>> GetAllMembers()
        {
            var response = await _httpClient.GetAsync("/api/member");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatMemberResponse>();
            }
            var membersList = await response.Content.ReadFromJsonAsync<List<ChatMemberResponse>>();
            return membersList;
        }

        public async Task<ServiceResponseViewModel<ChatMemberResponse>> GetMember(GetMemberDto getMemberDto)
        {
            ServiceResponseViewModel<ChatMemberResponse> getMemberResponse = new();
            var response = await _httpClient.GetAsync($"api/Member/{getMemberDto.SearchedUsername}");

            if (!response.IsSuccessStatusCode)
            {
                getMemberResponse.Success = false;
                getMemberResponse.Message = await response.Content.ReadAsStringAsync();
                return getMemberResponse;
            }

            getMemberResponse.Success = true;
            getMemberResponse.Data = await response.Content.ReadFromJsonAsync<ChatMemberResponse>();
            return getMemberResponse;
        }

    }
}
