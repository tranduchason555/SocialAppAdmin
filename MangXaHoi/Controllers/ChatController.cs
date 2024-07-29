
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private ChatService chatService;
        private DatabaseContext db;
        public ChatController(ChatService _contentfriendshipService, DatabaseContext _db)
        {
            chatService = _contentfriendshipService;
            this.db = _db;
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] Chat chat)
        {
            try
            {
                return Ok(new
                {
                    status = chatService.Create(chat)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
