using AutoMapper;
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
        private readonly IMapper _mapper;

        public ChatRoomController(IChatRoomRepository chatRoomRepository, IMapper mapper)
        {
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
        }

        [Authorize]
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
        public async Task<IActionResult> GetChatRoom([FromRoute] Guid chatId)
        {
            try
            {
                var chatRoom = await _chatRoomRepository.GetChatRoomAsync(chatId);
                var chatRoomDto = _chatRoomRepository.ConvertChatToDto(chatRoom);
                return Ok(chatRoomDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{chatId:guid}/members")]
        public async Task<IActionResult> GetChatRoomMembers([FromRoute] Guid chatId)
        {
            try
            {
                var chatRoomMembers = await _chatRoomRepository.GetChatRoomMembersAsync(chatId);
                return Ok(chatRoomMembers);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            var newChat = new Chat()
            {
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,
            };

            foreach (var member in chatRequest.ChatMembersIds)
            {
                newChat.AddMember(member);
            }

            var result = await _chatRoomRepository.CreateChatRoomAsync(newChat);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        [Route("{chatId:guid}")]
        public async Task<IActionResult> AddMemberToChat([FromRoute] Guid chatId, [FromBody] Guid userId)
        {
            var chatRoom = await _chatRoomRepository.GetChatRoomAsync(chatId);

            chatRoom.AddMember(userId);

            await _chatRoomRepository.UpdateChatRoomAsync(chatRoom);

            return Ok("Member added");
        }
    }
}
