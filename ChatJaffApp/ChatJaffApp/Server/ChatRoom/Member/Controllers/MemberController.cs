using ChatJaffApp.Server.ChatRoom.Member.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ChatJaffApp.Server.ChatRoom.Controllers.ChatRoomController;

namespace ChatJaffApp.Server.ChatRoom.Member.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMemberToChat(AddMemberDto addMemberDto)
        {
            //List<AddMemberResponse> mockMembersDb = new();
            //var newMember = _mapper.Map<AddMemberResponse>(addMemberDto);
            //mockMembersDb.Add(newMember);
            List<AddMemberResponse> mockMembersDb = new()
            {
                new AddMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Batman42"
                },
                new AddMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Catwoman"
                },
                new AddMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Randy"
                },
                new AddMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Bandy"
                },
                new AddMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Wolverine"
                }
            };

            var requestedMember = mockMembersDb
                .FirstOrDefault(member => string.Equals(member.Username, addMemberDto.SearchedUsername, StringComparison.OrdinalIgnoreCase));

            return requestedMember != null ? Ok(requestedMember) : BadRequest("Could not find member.");
        }
    }
}
