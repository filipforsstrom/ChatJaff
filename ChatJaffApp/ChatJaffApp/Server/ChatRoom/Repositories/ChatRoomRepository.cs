using ChatJaffApp.Client.ChatRoom.CreateChat.Models;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data;
using Microsoft.EntityFrameworkCore;

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
            return ChatRooms.Where(x => x.ChatMembers.Any(cm => cm.UserId == memberId));
        }

        public bool AddMemberToChat(AddMemberToChatDto addMemberToChatDto)
        {
            var chatRoom = _context.ChatRooms.FirstOrDefault(c => c.Id == addMemberToChatDto.ChatId);
            //chatRoom.ChatMembersIds.Add(chatMemberData.UserId);

            //if(!chatRoom.ChatMembersIds.Contains(chatMemberData.UserId))
            //{
            //    return false;
            //}

            return true;
        }

        public async Task<Chat> GetChatRoomAsync(Guid chatId)
        {
            var chatRoom = await _context.ChatRooms.Include(c => c.ChatMembers).ThenInclude(m => m.Member).FirstOrDefaultAsync(c => c.Id == chatId);
            if(chatRoom == null)
            {
                return new Chat();
            }

            return chatRoom;
            
        }

        public async Task UpdateChatRoomAsync(Chat chatRoomToUpdate)
        {
            _context.ChatRooms.Update(chatRoomToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Data.Models.Member>> GetChatMembers(Guid chatId)
        {
            var chatMembers = _context.ChatMembers.Where(cm => cm.ChatId == chatId).Select(cm => cm.Member).ToList();
            return chatMembers;
        }
    }
}