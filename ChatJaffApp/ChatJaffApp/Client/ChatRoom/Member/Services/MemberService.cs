using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Contracts;
using ChatJaffApp.Client.ChatRoom.Member.Models;
using ChatJaffApp.Client.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ChatJaffApp.Client.ChatRoom.Member.Services
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient _httpClient;

        public MemberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponseViewModel<ChatMemberResponse>> AddChatMember(AddMemberDto addMemberDto)
        {
            ServiceResponseViewModel<ChatMemberResponse> addMemberResponse = new();
            var response = await _httpClient.PostAsJsonAsync("api/chatroom/addmembertochat", addMemberDto);

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

        public async Task<ServiceResponseViewModel<ChatMemberResponse>> GetChatMember(InviteMemberDto invMemberDto)
        {
            ServiceResponseViewModel<ChatMemberResponse> getMemberResponse = new();
            var response = await _httpClient.PostAsJsonAsync("api/member/getchatmember", invMemberDto);

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
