using ChatApp.Business;
using ChatApp.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatService.GetChatsAsync();
            return Ok(chats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(int id)
        {
            var chat = await _chatService.GetChatByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(Chat chat)
        {
            await _chatService.CreateChatAsync(chat);
            return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int id, [FromBody] int userId)
        {
            try
            {
                await _chatService.DeleteChatAsync(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }

}
