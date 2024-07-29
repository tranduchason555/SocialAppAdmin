
using MangXaHoi.Helpers;
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MangXaHoi.Controllers
{
    [Route("api/storyfriendship")]
    public class StoryfriendshipController : Controller
    {
        private StoryfriendshipService storyfriendshipService;
        IWebHostEnvironment webHostEnvironment;
        private DatabaseContext db;
        public StoryfriendshipController(StoryfriendshipService _storyfriendshipService, DatabaseContext _db, IWebHostEnvironment _webHostEnvironment)
        {
            storyfriendshipService = _storyfriendshipService;
            this.db = _db;
            this.webHostEnvironment= _webHostEnvironment;
        }
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("create2")]
        public IActionResult Create2(IFormFile file, string strProduct)
        {
            try
            {
                // Ensure file and strProduct are not null
                if (file == null || string.IsNullOrEmpty(strProduct))
                {
                    return BadRequest(new { error = "File or product data is missing" });
                }

                // Log file information
                Debug.WriteLine("File info");
                Debug.WriteLine("name: " + file.FileName);
                Debug.WriteLine("type: " + file.ContentType);
                Debug.WriteLine("size: " + file.Length);

                // Generate file name and path
                var fileName = FileHelper.generateFileName(file.FileName);
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);

                // Save file to server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Log product string
                Debug.WriteLine(strProduct);

                // Deserialize product string to object
                var story = JsonConvert.DeserializeObject<Story>(strProduct);

                // Ensure product is not null
                if (story == null)
                {
                    return BadRequest(new { error = "Invalid product data" });
                }

                // Set product photo file name
                story.Photo = fileName;

                // Create product using service
                var result = storyfriendshipService.Create(story);

                // Return success response
                return Ok(new { status = result });
            }
            catch (Exception ex)
            {
                // Log exception message
                Debug.WriteLine(ex.Message);
                return BadRequest(new { error = ex.Message });
            }
        }
        [Produces("application/json")]
        [Route("findByUserid/{userid}")]
        public IActionResult findByUserid(int userid)
        {
            try
            {


                return Ok(storyfriendshipService.findByUserid(userid));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByUserid1/{userid}")]
        public IActionResult findByUserid1(int userid)
        {
            try
            {


                return Ok(storyfriendshipService.findByUserid1(userid));


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
        public IActionResult Create([FromBody] Story story)
        {
            try
            {
                return Ok(new
                {
                    status = storyfriendshipService.Create(story)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
