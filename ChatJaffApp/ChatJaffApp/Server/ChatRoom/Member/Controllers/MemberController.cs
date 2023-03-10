using ChatJaffApp.Server.ChatRoom.Member.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChatJaffApp.Server.ChatRoom.Controllers.ChatRoomController;
using ChatJaffApp.Server.ChatRoom.Member.Contracts;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Client.Member.Contracts;

namespace ChatJaffApp.Server.ChatRoom.Member.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<GetMemberDto> GetAllMemberss()
        {
            var allMembersEntities = _memberRepository.GetAllMembers();
            var allMembersDto = new List<GetMemberDto>();

            foreach(var member in allMembersEntities)
            {
                allMembersDto.Add(new GetMemberDto { UserId = member.Id, UserName = member.UserName});
            }

            return allMembersDto;
        }

        [HttpGet]
        [Route("{searchedUsername}")]
        public async Task<IActionResult> GetMember([FromRoute]string searchedUsername)
        {
            try
            {
                var requestedMember = await _memberRepository.GetMember(searchedUsername);
                return Ok(requestedMember);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
