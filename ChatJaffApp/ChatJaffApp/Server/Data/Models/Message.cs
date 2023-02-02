using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatJaffApp.Server.Data.Models
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTime Sent { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(Chat))]
        public Guid ChatId { get; set; }

        public Chat Chat { get; set; }
        public User User { get; set; }
    }
}
