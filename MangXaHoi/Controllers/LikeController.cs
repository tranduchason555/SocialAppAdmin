
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/like")]
    public class LikeController : Controller
    {
        private LikeService likeService;
        private DatabaseContext db;
        public LikeController(LikeService _contentfriendshipService, DatabaseContext _db)
        {
            likeService = _contentfriendshipService;
            this.db = _db;
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] Like like)
        {
            try
            {
                return Ok(new
                {
                    status = likeService.Create(like)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByContentid/{contentid}")]
        public IActionResult findByContentid(int contentid)
        {
            try
            {


                return Ok(likeService.findByContentid(contentid));


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
                    status = likeService.Delete(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
