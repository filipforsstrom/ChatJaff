﻿using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class Chat : IChat
    {
        public Guid Id { get; set; }
        public List<Guid> ChatMembersIds { get; set; }
        public string Creator { get; set; }
        public bool Encrypted { get; set; }
        public string ChatName { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
