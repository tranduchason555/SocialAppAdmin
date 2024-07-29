
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/friendships")]
    public class FriendshipController : Controller
    {
        private FriendshipService friendshipService;
        private DatabaseContext db;
        public FriendshipController(FriendshipService _friendshipService, DatabaseContext _db)
        {
            friendshipService = _friendshipService;
            this.db = _db;
        }
        [Produces("application/json")]
        [Route("findByUserid/{userid}")]
        public IActionResult findByUserid(int userid)
        {
            try
            {


                return Ok(friendshipService.findByUserId(userid));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("update")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Update([FromBody] Friendship frienship)
        {
            try
            {
                return Ok(new
                {
                    status = friendshipService.Update(frienship)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("FindByEmail/{email}/{userid}")]
        public IActionResult FindByEmail(string email,int userid)
        {
            try
            {


                return Ok(friendshipService.FindByEmail(email,userid));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByFullnameship/{fullname}/{userid}")]
        public IActionResult findByFullnameship(string fullname, int userid)
        {
            try
            {


                return Ok(friendshipService.findByFullnameship(fullname, userid));
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByFriendship/{userid}")]
        public IActionResult findByFriendship(int userid)
        {
            try
            {


                return Ok(friendshipService.findByFriendship(userid));


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
        public IActionResult Create([FromBody] Friendship friendship)
        {
            try
            {
                return Ok(new
                {
                    status = friendshipService.Create(friendship)
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
                    status = friendshipService.Remove(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
