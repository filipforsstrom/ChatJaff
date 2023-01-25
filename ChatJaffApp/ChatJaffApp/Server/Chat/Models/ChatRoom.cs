using ChatJaffApp.Server.Chat.Contracts;

namespace ChatJaffApp.Server.Chat.Models
{
    public class ChatRoom : IChatRoom
    {
        public List<Guid> ChatMembersIds { get; set; }
        public bool Encrypted { get; set; }
        public string ChatName { get; set; }
    }
}
