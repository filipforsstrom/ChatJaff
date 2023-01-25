using ChatJaffApp.Server.Chat.ChatRoom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.Chat.ChatRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        //fake db
        public static List<MockChatModel> ChatModel { get; set; } = new();

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            MockChatModel newChat = new MockChatModel()
            {
                Id = 23,
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,

            };

            foreach (var member in chatRequest.Chatmembers)
            {
                newChat.Members.Add(member.Username);
            }

            ChatModel.Add(newChat);


            return Ok(newChat.Id);
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
