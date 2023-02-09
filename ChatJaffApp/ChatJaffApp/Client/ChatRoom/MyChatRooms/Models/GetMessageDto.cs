namespace ChatJaffApp.Client.ChatRoom.MyChatRooms.Models
{
    public class GetMessageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime Sent { get; set; }
    }
}
