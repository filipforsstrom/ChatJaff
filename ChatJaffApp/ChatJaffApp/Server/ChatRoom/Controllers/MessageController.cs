using ChatJaffApp.Client.ChatRoom.Pages;
using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Encryption;
using ChatJaffApp.Server.ChatRoom.Models;
using ChatJaffApp.Server.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ChatJaffApp.Server.ChatRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatKeyRepository _chatKeyRepository;

        public MessageController( IMessageRepository messageRepository, IChatKeyRepository chatKeyRepository)
        {
            _messageRepository = messageRepository;
            _chatKeyRepository = chatKeyRepository;
        }

        [Authorize]
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid id) 
        {
            await _messageRepository.DeleteMessage(id);

            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("{messageId:guid}")]
        public async Task<IActionResult> EditMessage([FromRoute]Guid messageId, [FromBody] EditMessageDto newMessage)
        {
            string returnMessage = newMessage.EditedMessage;
            var message = await _messageRepository.GetMesssage(messageId);

            var chatKey = await _chatKeyRepository.GetChatKeyAsync(message.ChatId);

            EncryptionHelper encryptionHelper = new();
            string encryptedMessage = encryptionHelper.EncryptMessage(newMessage.EditedMessage, chatKey);

            newMessage.EditedMessage = encryptedMessage;

            var response = await _messageRepository.EditMessage(newMessage);
            if(!response)
            {
                return BadRequest();
            }
            return Ok(returnMessage);
        }
    }
}
