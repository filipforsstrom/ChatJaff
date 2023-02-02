using ChatJaffApp.Server.ChatRoom.Member.Contracts;
using ChatJaffApp.Server.ChatRoom.Member.Models;
using ChatJaffApp.Server.Data;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace ChatJaffApp.Server.ChatRoom.Member.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly JaffDbContext _context;

        public MemberRepository(JaffDbContext context)
        {
            _context = context;
        }

        public async Task<GetMemberDto> GetMember(string searchedUserName)
        {
            var user = await _context.Members.FirstOrDefaultAsync(m => m.UserName.ToLower() == searchedUserName.ToLower());

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            GetMemberDto getMemberDto = new()
            {
                UserId = user.Id,
                UserName = user.UserName,
            };

            return getMemberDto;

        }
    }
}
