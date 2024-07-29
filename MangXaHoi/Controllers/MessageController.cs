
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/message")]
    public class MessageController : Controller
    {
        private MessageService messageService;
        private DatabaseContext db;
        public MessageController(MessageService _messageService, DatabaseContext _db)
        {
            messageService = _messageService;
            this.db = _db;
        }
        [Produces("application/json")]
        [Route("findByUserid/{userid}")]
        public IActionResult findByUserid(int userid)
        {
            try
            {

                return Ok(messageService.findByUserId(userid));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByFullname/{userId}/{fullname}")]
        public IActionResult findByFullname(int userId, string fullname)
        {
            try
            {

                return Ok(messageService.findbyfullname(userId,fullname));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByFullname1/{fullname}")]
        public IActionResult findByFullname1(string fullname)
        {
            try
            {

                return Ok(messageService.findbyfullname1(fullname));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] Message message)
        {
            try
            {
                return Ok(new
                {
                    status = messageService.Create(message)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(new
                {
                    status = messageService.Delete(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
