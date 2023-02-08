using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ChatJaffApp.Server.Data.Models
{
    public class ChatKey
    {
        [Key]
        public Guid ChatRoomId { get; set; }
        [Required]
        public string? Key { get; set; }
    }
}
