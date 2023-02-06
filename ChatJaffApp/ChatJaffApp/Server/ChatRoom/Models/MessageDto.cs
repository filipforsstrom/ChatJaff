namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string? Content { get; set; }
        public DateTime Sent { get; set; }
    }
}
