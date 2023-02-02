using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class Chat : IChat
    {
        public Guid Id { get; set; }
        public ICollection<ChatMember> ChatMembers { get; }
        public string? Creator { get; set; }
        public bool Encrypted { get; set; }
        public string? ChatName { get; set; }
        public ICollection<Message> Messages { get; set; }

        public void AddMember(Guid userId)
        {
            ChatMembers.Add(new ChatMember {UserId = userId});
        }

    }
   
}
