﻿namespace ChatJaffApp.Server.Identity.Models
{
    public class ChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
