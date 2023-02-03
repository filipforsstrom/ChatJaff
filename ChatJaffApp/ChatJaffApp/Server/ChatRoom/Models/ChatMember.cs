using ChatJaffApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class ChatMember
    {
        [ForeignKey(nameof(Member))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Chat))]
        public Guid? ChatId { get; set; }

        public Data.Models.Member Member { get; set; }
        public Chat Chat { get; set; }
    }
}
