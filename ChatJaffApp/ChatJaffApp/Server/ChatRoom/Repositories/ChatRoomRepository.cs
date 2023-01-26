﻿using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.Data;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly JaffDbContext _context;

        public static List<IChat> ChatRooms { get; set; } = new();

        public ChatRoomRepository(JaffDbContext context)
        {
            
            _context = context;
        }

        public async Task<Guid> CreateChatRoomAsync(IChat chatRoom)
        {
            chatRoom.Id = Guid.NewGuid();
            ChatRooms.Add(chatRoom);
            var createdRoom = ChatRooms.LastOrDefault();
            return createdRoom.Id;
        }
    }
}