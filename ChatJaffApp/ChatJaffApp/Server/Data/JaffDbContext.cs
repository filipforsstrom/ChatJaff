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


	}
}

