﻿using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Client.Chat.CreateChat.Models
{
    public class InviteMemberDTO
    {
        [Required(ErrorMessage = "Search field must not be empty.")]
        [MinLength(2, ErrorMessage = "Username must be longer than 1 character")]
        public string? SearchedUsername { get; set; } = string.Empty;
    }
}
