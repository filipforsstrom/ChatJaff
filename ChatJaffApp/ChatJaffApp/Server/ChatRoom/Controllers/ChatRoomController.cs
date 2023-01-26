using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.ChatRoom.Controllers
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

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<IChat> GetAllChats()
        {
            var tempList = _chatRoomRepository.GetAllChatRooms();
            return tempList;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            var newChat = new Chat()
            {
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,
                ChatMembersIds = chatRequest.ChatMembersIds
            };

            var result = await _chatRoomRepository.CreateChatRoomAsync(newChat);

            return Ok(result);
        }

    }
}
