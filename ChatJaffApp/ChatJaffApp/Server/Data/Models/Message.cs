using ChatJaffApp.Server.Data.Contracts;

namespace ChatJaffApp.Server.Data.Models
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTime Sent { get; } = DateTime.UtcNow;
        public Guid ChatId { get; set; }
    }
}
