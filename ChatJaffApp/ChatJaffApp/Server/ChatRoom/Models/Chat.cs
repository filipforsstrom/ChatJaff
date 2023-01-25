using ChatJaffApp.Server.ChatRoom.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class Chat : IChat
    {
        public List<Guid> ChatMembersIds { get; set; }
        public bool Encrypted { get; set; }
        public string ChatName { get; set; }
    }
}
