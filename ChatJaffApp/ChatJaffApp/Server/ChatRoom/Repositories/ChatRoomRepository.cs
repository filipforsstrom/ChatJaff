using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Client.ChatRoom.Pages;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Controllers;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatJaffApp.Server.ChatRoom.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly JaffDbContext _context;

        public ChatRoomRepository(JaffDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateChatRoomAsync(Chat chatRoom)
        {
            _context.ChatRooms.Add(chatRoom);
            var result = await _context.SaveChangesAsync();

            return chatRoom.Id;

        }
        public IEnumerable<IChat> GetAllChatRooms()
        {
            return _context.ChatRooms.ToList();
        }
        public IEnumerable<IChat> GetMyChatRooms(Guid memberId)
        {
            var chatRooms = _context.ChatRooms.Where(x => x.ChatMembers.Any(cm => cm.UserId == memberId)).ToList();
            return chatRooms;
        }

        public async Task<Chat> GetChatRoomAsync(Guid chatId)
        {
            var chatRoom = await _context.ChatRooms.Include(c => c.ChatMembers)
                .ThenInclude(m => m.Member)
                .Include(c => c.Messages.OrderBy(m => m.Sent))
                .ThenInclude(m => m.Member)
                .FirstOrDefaultAsync(c => c.Id == chatId);

            if(chatRoom == null)
            {
                throw new KeyNotFoundException();
            }

            return chatRoom;
        }

        public async Task UpdateChatRoomAsync(Chat chatRoomToUpdate)
        {
            _context.ChatRooms.Update(chatRoomToUpdate);
            await _context.SaveChangesAsync();
        }

        public ChatRoomDto ConvertChatToDto(Chat chatRoom)
        {
            ChatRoomDto chatRoomDto = new ChatRoomDto();
            chatRoomDto.ChatMembers = chatRoom.ChatMembers.Select(cm => new ChatMemberDto { UserId = cm.UserId, Username = cm.Member.UserName }).ToList();
            chatRoomDto.Messages = chatRoom.Messages.Select(m => new MessageDto { Id = m.Id, Content = m.Content, Sent = m.Sent, UserName = m.Member.UserName, UserId = m.Member.Id }).ToList();
            chatRoomDto.Encrypted = chatRoom.Encrypted;
            chatRoomDto.ChatName = chatRoom.ChatName;
            chatRoomDto.Id = chatRoom.Id;
            chatRoomDto.CreatorId = chatRoom.CreatorId;

            return chatRoomDto;
        }

        public async Task<bool> DeleteChatRoom(Chat chatRoom)
        {
            _context.ChatRooms.Remove(chatRoom);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}