﻿using ChatJaffApp.Server.Data.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Contracts
{
    public interface IChatRoomRepository
    {
        Task<Guid> CreateChatRoomAsync(IChat chatRoom);
        IEnumerable<IChat> GetAllChatRooms();
        Task SaveMessage(IMessage message);
    }
}
