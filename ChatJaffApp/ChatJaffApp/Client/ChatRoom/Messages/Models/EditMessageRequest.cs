namespace ChatJaffApp.Client.ChatRoom.Messages.Models
{
    public class EditMessageRequest
    {
        public Guid MessageId { get; set; }
        public string EditedMessage { get; set; }
    }
}
