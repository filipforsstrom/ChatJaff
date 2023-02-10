using ChatJaffApp.Server.ChatRoom.Contracts;
using ChatJaffApp.Server.ChatRoom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.ChatRoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController( IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
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
            var response = await _messageRepository.EditMessage(newMessage);
            if(response is null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
