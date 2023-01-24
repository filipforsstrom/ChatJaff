using ChatJaffApp.Client.Chat.CreateChat.Models;

namespace ChatJaffApp.Client.Chat.Member.Models
{
    public class InviteMemberResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public ChatMemberResponse MemberData { get; set; }
    }
}
