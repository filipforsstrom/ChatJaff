namespace ChatJaffApp.Client.Chat.CreateChat.Models
{
    public class CreateChatRequest
    {
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<ChatMember>? Chatmembers { get; set; }
    }
}
