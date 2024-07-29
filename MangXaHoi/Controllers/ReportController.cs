
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/report")]
    public class ReportController : Controller
    {
        private ReportService reportService;
        private DatabaseContext db;
        public ReportController(ReportService _reportService, DatabaseContext _db)
        {
            reportService = _reportService;
            this.db = _db;
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] Report message)
        {
            try
            {
                return Ok(new
                {
                    status = reportService.Create(message)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    
    }
}
