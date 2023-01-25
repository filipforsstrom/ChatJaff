using ChatJaffApp.Server.Chat.Contracts;
using ChatJaffApp.Server.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            var newChat = new ChatRoom()
            {
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,
                ChatMembersIds = chatRequest.ChatMembersIds
            };

            var result = await _chatRoomRepository.CreateChatRoomAsync(newChat);

            return Ok(result);
        }


        public class MockChatModel
        {
            public int Id { get; set; }
            public List<string> Members { get; set; } = new();
            public string? Creator { get; set; }
            public List<string> Messages { get; set; } = new();
            public bool Encrypted { get; set; }
            public string? ChatName { get; set; }
        }
    }
}
