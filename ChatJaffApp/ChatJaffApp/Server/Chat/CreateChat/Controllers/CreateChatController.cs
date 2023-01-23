using ChatJaffApp.Client.Chat.CreateChat.Models;
using ChatJaffApp.Server.Chat.CreateChat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatJaffApp.Server.Chat.CreateChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateChatController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateChat(CreateChatDTO chatRequest)
        {
            return null;
        }
    }
}
