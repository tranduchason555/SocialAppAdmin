
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/save")]
    public class SaveController : Controller
    {
        private SaveService saveService;
        private DatabaseContext db;
        public SaveController(SaveService _savepService, DatabaseContext _db)
        {
            saveService = _savepService;
            this.db = _db;
        }
        [Produces("application/json")]
        [Route("findByContentid/{contentid}")]
        public IActionResult findByContentid(int contentid)
        {
            try
            {


                return Ok(saveService.findByContentid(contentid));


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
        public IActionResult Create([FromBody] Safe save)
        {
            try
            {
                return Ok(new
                {
                    status = saveService.Create(save)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByUserid/{userid}")]
        public IActionResult findByUserid(int userid)
        {
            try
            {


                return Ok(saveService.findByUserId(userid));


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
                    status = saveService.Delete(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
