﻿using ChatJaffApp.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatJaffApp.Server.ChatRoom.Models
{
    public class ChatMember
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Chat))]
        public Guid? ChatId { get; set; }

        public User User { get; set; }
        public Chat Chat { get; set; }
    }
}