﻿using ChatJaffApp.Server.Chat.Member.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.Chat.Member.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddMemberToChat(AddMemberDto addMemberDto)
        {
            List<AddMemberResponse> mockMembersDb = new()
            {
                new AddMemberResponse
                {
                    UserId = "192391n3j1j23",
                    Username = "Batman42"
                },
                new AddMemberResponse
                {
                    UserId = "192kjasdjas1j23",
                    Username = "Catwoman"
                },
                new AddMemberResponse
                {
                    UserId = "19o1j412k12",
                    Username = "Randy"
                },
                new AddMemberResponse
                {
                    UserId = "19o1j412k12",
                    Username = "Bandy"
                },
                new AddMemberResponse
                {
                    UserId = "19o1j412k12",
                    Username = "Wolverine"
                }
            };

            var requestedMember = mockMembersDb
                .FirstOrDefault(member => string.Equals(member.Username, addMemberDto.SearchedUsername, StringComparison.OrdinalIgnoreCase));

            return requestedMember != null ? Ok(requestedMember) : BadRequest("Could not find member.");
        }
    }
}