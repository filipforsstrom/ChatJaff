using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class Chat : IChat
    {
        public Guid Id { get; set; }
        public List<ChatMember> ChatMembers { get; set; } = new();
        public string? Creator { get; set; }
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public ICollection<Message> Messages { get; set; }
        [ForeignKey(nameof(Member))]
        public Guid UserId { get;  set; }

        public Data.Models.Member Member { get; set; }



        public void AddMember(Guid userId)
        {
            ChatMembers.Add(new ChatMember {UserId = userId});
        }
    }
   
}
