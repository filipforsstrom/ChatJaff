using ChatJaffApp.Server.ChatRoom.Member.Contracts;
using ChatJaffApp.Server.ChatRoom.Member.Models;
using ChatJaffApp.Server.Data.Models;
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

        public async Task AddMemberToDb(Guid userId, string userName)
        {
            Data.Models.Member member = new();
            member.UserName = userName;
            member.Id = userId;
            try
            {
                var result = await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw new DbUpdateException("Something went wrong");
            }
            catch(Exception ex) { 
                throw new Exception(ex.Message);
            }
        
        }

        public async Task ChangeMemberUserName(Guid userId, string newUserName)
        {
            var memberToUpdate = await _context.Members.FindAsync(userId);
            
            if(memberToUpdate == null) 
            {
                return;
            }

            try
            {
                memberToUpdate.UserName = newUserName;
                var result = _context.Members.Update(memberToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Something went wrong");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ChatJaffApp.Server.Data.Models.Member> GetAllMembers()
        {
            return _context.Members.ToList();
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
