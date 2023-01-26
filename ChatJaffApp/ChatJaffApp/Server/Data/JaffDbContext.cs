using System;
using Microsoft.EntityFrameworkCore;
using ChatJaffApp.Server.ChatRoom.Models;

namespace ChatJaffApp.Server.Data
{
	public class JaffDbContext : DbContext
	{
		public DbSet<Chat> ChatRooms { get; set; }

		public JaffDbContext(DbContextOptions<JaffDbContext> options) : base(options)
		{

		}


	}
}

