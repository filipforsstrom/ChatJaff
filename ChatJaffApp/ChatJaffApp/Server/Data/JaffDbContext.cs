using System;
using Microsoft.EntityFrameworkCore;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;

namespace ChatJaffApp.Server.Data
{
	public class JaffDbContext : DbContext
	{
		public DbSet<Chat> ChatRooms { get; set; }
		public DbSet<Message> Messages { get; set; }

		public JaffDbContext(DbContextOptions<JaffDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SeedChatRooms(modelBuilder);
		}

        private void SeedChatRooms(ModelBuilder modelBuilder)
        {
			List<Chat> chats = new List<Chat>()
			{
				new Chat() {
					Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
					ChatName = "Chat 1",
					Creator = "Filip",
					Encrypted= false,
				},
                new Chat() {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ChatName = "Chat 2",
                    Creator = "Baronen",
                    Encrypted = false,
                },
            };

			modelBuilder.Entity<Chat>().HasData(chats);
        }
    }
}

