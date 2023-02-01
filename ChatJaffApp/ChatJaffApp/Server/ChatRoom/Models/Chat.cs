using ChatJaffApp.Server.ChatRoom.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class Chat : IChat
    {
        public Guid Id { get; set; }
        public List<ChatMember> ChatMembersIds { get; } = new();
        public string? Creator { get; set; }
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        //public List<string> Messages { get; set; } = new();

        public void AddMember(Guid userId)
        {
            ChatMembersIds.Add(new ChatMember { Id = userId });
        }

    }

    public class ChatMember
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
    }
}
