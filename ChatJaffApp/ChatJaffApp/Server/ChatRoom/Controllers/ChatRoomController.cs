using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMyChats(Guid id)
        {
            try
            {
                await Task.Delay(1000);
                var memberChatRooms = _chatRoomRepository.GetMyChatRooms(id);
                return Ok(memberChatRooms);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("{chatId:guid}")]
        public async Task<IActionResult> GetChatMembers([FromRoute]Guid chatId)
        {
            try
            {
                var chatRoom = await _chatRoomRepository.GetChatRoomAsync(chatId);

                var chatMembers = chatRoom.ChatMembers
                    .Select(cm => new ChatMemberDto { UserId = cm.UserId, Username = cm.Member.UserName })
                    .ToList();

                return Ok(chatMembers);
            }
            catch (Exception ex)
            {
               return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            var newChat = new Chat()
            {
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,
            };

            foreach (var members in chatRequest.ChatMembersIds)
            {
                newChat.AddMember(members);
            }

            var result = await _chatRoomRepository.CreateChatRoomAsync(newChat);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        [Route("{chatId:guid}")]
        public async Task<IActionResult> AddMemberToChat([FromRoute] Guid chatId, [FromBody] Guid userId)
        {
            var addMemberToChatDto = new AddMemberToChatDto
            {
                ChatId = chatId,
                UserId = userId
            };

            var chatRoom = await _chatRoomRepository.GetChatRoomAsync(chatId);

            chatRoom.AddMember(userId);

            await _chatRoomRepository.UpdateChatRoomAsync(chatRoom);

            return Ok("Member added");
        }
    }

    public class ChatMemberDto
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
    }
}
