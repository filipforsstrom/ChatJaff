namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class AddMemberToChatDto
    {
        public Guid UserId { get; set; }
        public Guid? ChatId { get; set; }
    }
}
