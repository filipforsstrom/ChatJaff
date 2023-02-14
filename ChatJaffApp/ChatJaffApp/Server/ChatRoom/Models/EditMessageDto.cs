namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class EditMessageDto
    {
        public Guid MessageId { get; set; }
        public string EditedMessage { get; set; }
        public bool Encrypted { get; set; }
    }
}
