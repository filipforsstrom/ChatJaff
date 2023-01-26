namespace ChatJaffApp.Client.ChatRoom.CreateChat.Models
{
    public class CreateChatDto
    {
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public List<Guid>? ChatMembersIds { get; set; }
    }
}
