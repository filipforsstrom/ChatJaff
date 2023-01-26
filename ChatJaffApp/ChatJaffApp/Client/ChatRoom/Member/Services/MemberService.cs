using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Member.Contracts;
using ChatJaffApp.Client.ChatRoom.Member.Models;
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
        public async Task<InviteMemberResponse> AddMemberToChat(InviteMemberDto invMemberDto)
        {
            InviteMemberResponse invMemberResponse = new();
            var response = await _httpClient.PostAsJsonAsync("api/member/addmembertochat", invMemberDto);

            if (!response.IsSuccessStatusCode)
            {
                invMemberResponse.Success = false;
                invMemberResponse.Message = await response.Content.ReadAsStringAsync();
                return invMemberResponse;
            }

            invMemberResponse.Success = true;
            invMemberResponse.MemberData = await response.Content.ReadFromJsonAsync<ChatMemberResponse>();
            invMemberResponse.Message = "User added";
            return invMemberResponse;
        }
    }
}
