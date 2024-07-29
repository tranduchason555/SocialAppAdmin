
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private CommentService commentService;
        private DatabaseContext db;
        public CommentController(CommentService _contentfriendshipService, DatabaseContext _db)
        {
            commentService = _contentfriendshipService;
            this.db = _db;
        }
        [Produces("application/json")]
        [Route("findByContentid/{contentid}")]
        public IActionResult findByContentid(int contentid)
        {
            try
            {


                return Ok(commentService.findByContentid(contentid));


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
        public IActionResult Create([FromBody] Comment comment)
        {
            try
            {
                return Ok(new
                {
                    status = commentService.Create(comment)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByMessageid/{messageid}")]
        public IActionResult findByMessageid(int messageid)
        {
            try
            {


                return Ok(commentService.findByMessageid(messageid));
                

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
