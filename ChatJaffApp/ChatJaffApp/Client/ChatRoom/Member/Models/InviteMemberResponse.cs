using ChatJaffApp.Client.ChatRoom.CreateChat.Models;

namespace ChatJaffApp.Client.ChatRoom.Member.Models
{
    public class InviteMemberResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public ChatMemberResponse MemberData { get; set; }
    }
}
