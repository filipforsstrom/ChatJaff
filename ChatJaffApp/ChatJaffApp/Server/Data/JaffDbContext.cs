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

        public DbSet<ChatMember> ChatMembers { get; set; }

        public DbSet<Member> Members { get; set; }

        public JaffDbContext(DbContextOptions<JaffDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedChatRooms(modelBuilder);
            SeedUsers(modelBuilder);
            SeedChatMembers(modelBuilder);
            SeedMessages(modelBuilder);

            modelBuilder.Entity<ChatMember>().HasKey(cm => new { cm.ChatId, cm.UserId });
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

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            List<Member> members = new()
            {
                new Member () { Id = Guid.Parse("9CEAA7AB-1C67-4ED4-A86A-6BD01DF6C310"), UserName = "member1"},
                new Member () { Id = Guid.Parse("b8381d75-d110-42f9-85e5-9c92a062fbc8"), UserName = "member2"},
                new Member () { Id = Guid.Parse("9F7E8005-873A-489D-B569-AFB17A58B051"), UserName = "Batman42"},
                new Member () { Id = Guid.Parse("C4423797-979C-4A72-A7F8-53499AAB5469"), UserName = "Catwoman"},
                new Member () { Id = Guid.Parse("{28E44613-4ACE-474B-965F-E589E72AAF46}"), UserName = "Randy"},
                new Member () { Id = Guid.Parse("{E36D119F-F8E3-4BD1-A3F1-0845F8CBE529}"), UserName = "Bandy"},
                new Member () { Id = Guid.Parse("{95A471EE-E8A7-44D4-BB13-C5090FB1A5CE}"), UserName = "Silvio"},
            };

            modelBuilder.Entity<Member>().HasData(members);
        }
        private void SeedMessages(ModelBuilder modelBuilder)
        {
            List<Message> messages = new()
            {
                new Message() {
                    Id = Guid.Parse("{ABDA0517-7354-49FC-9239-8E350B42FB9D}"),
                    UserId = Guid.Parse("9F7E8005-873A-489D-B569-AFB17A58B051") ,
                    ChatId= Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = " Cathy, Meet me at the batcave girrl"

                },
                new Message() {
                    Id = Guid.Parse("{439F95CC-B2D6-4375-9281-6A5C65BA3806}"),
                    UserId = Guid.Parse("C4423797-979C-4A72-A7F8-53499AAB5469") ,
                    ChatId= Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = " Sure thang battyboy"
                }

            };

            modelBuilder.Entity<Message>().HasData(messages);

        }

        private void SeedChatMembers(ModelBuilder modelBuilder)
        {
            List<ChatMember> chatMembers = new()
            {
                new ChatMember() { UserId = Guid.Parse("9F7E8005-873A-489D-B569-AFB17A58B051"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                new ChatMember() { UserId = Guid.Parse("C4423797-979C-4A72-A7F8-53499AAB5469"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                new ChatMember() { UserId =  Guid.Parse("{28E44613-4ACE-474B-965F-E589E72AAF46}"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000001") },
                new ChatMember() { UserId =  Guid.Parse("{E36D119F-F8E3-4BD1-A3F1-0845F8CBE529}"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
                new ChatMember() { UserId =  Guid.Parse("{28E44613-4ACE-474B-965F-E589E72AAF46}"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000002") },
                new ChatMember() { UserId =  Guid.Parse("{95A471EE-E8A7-44D4-BB13-C5090FB1A5CE}"), ChatId = Guid.Parse("00000000-0000-0000-0000-000000000002") }
            };

            modelBuilder.Entity<ChatMember>().HasData(chatMembers);
        }
    }
}

