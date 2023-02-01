using Microsoft.AspNetCore.Authorization;
﻿using AutoMapper;
﻿using ChatJaffApp.Server.ChatRoom.Member.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ChatJaffApp.Server.ChatRoom.Controllers.ChatRoomController;

namespace ChatJaffApp.Server.ChatRoom.Member.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public List<GetMemberResponse> mockMembersDb = new()
            {
                new GetMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Batman42"
                },
                new GetMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Catwoman"
                },
                new GetMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Randy"
                },
                new GetMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Bandy"
                },
                new GetMemberResponse
                {
                    UserId = Guid.NewGuid(),
                    Username = "Wolverine"
                }
            };

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetChatMember(GetMemberDto addMemberDto)
        {
            //List<AddMemberResponse> mockMembersDb = new();
            //var newMember = _mapper.Map<AddMemberResponse>(addMemberDto);
            //mockMembersDb.Add(newMember);

            var requestedMember = mockMembersDb
                .FirstOrDefault(member => string.Equals(member.Username, addMemberDto.SearchedUsername, StringComparison.OrdinalIgnoreCase));

            return requestedMember != null ? Ok(requestedMember) : BadRequest("Could not find member.");
        }


    }
}
