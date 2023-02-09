using AutoMapper;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Encryption;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.ChatRoom.Repositories;
using ChatJaffApp.Server.Data.Models;
using ChatJaffApp.Server.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace ChatJaffApp.Server.ChatRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IChatKeyRepository _chatKeyRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository, IMapper mapper, SignInManager<ApplicationUser> signInManager, IChatKeyRepository chatKeyRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
            _signInManager = signInManager;
            _chatKeyRepository = chatKeyRepository;
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
        public async Task<IActionResult> GetMyChats()
        {
            var userInContext = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if(userInContext == null) { return NotFound(); }

            try
            {
                var memberChatRooms = _chatRoomRepository.GetMyChatRooms(Guid.Parse(userInContext.Id));
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
        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            var newChat = new Chat()
            {
                Encrypted = chatRequest.Encrypted,
                ChatName = chatRequest.ChatName,
                CreatorId=chatRequest.CreatorId,

            };

            foreach (var member in chatRequest.ChatMembersIds)
            {
                newChat.AddMember(member);
            }

            var result = await _chatRoomRepository.CreateChatRoomAsync(newChat);

            if (chatRequest.Encrypted)
            {
                EncryptionHelper encryptionHelper = new();
                var dbKey = encryptionHelper.GenerateDbKey();
                _chatKeyRepository.AddChatKeyAsync(result, dbKey);
            }

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

        [Authorize]
        [HttpDelete]
        [Route("{chatId:guid}")]
        public async Task<IActionResult> DeleteChat([FromRoute] Guid chatId)
        {
            try
            {
                var chatRoom = await _chatRoomRepository.GetChatRoomAsync(chatId);
                var result = await _chatRoomRepository.DeleteChatRoom(chatRoom);

                if (!result)
                {
                    return BadRequest();
                }

                if (chatRoom.Encrypted)
                {
                    try
                    {
                        // Ensure related messages are deleted
                        // Run after x days
                        await _chatKeyRepository.DeleteChatKey(chatRoom.Id);
                    }
                    catch (Exception)
                    {
                        // log error
                    }
                }

                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
