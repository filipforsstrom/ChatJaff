namespace ChatJaffApp.Client.ChatRoom.CreateChat.Models
{
    public class CreateChatDto
    {
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public string? Creator { get; set; }
        public List<Guid>? ChatMembersIds { get; set; }
        public Guid CreatorId { get; set; }
    }
}
