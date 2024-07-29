
using MangXaHoi.Helpers;
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace MangXaHoi.Controllers
{
    [Route("api/contentfriendship")]
    public class ContentfriendshipController : Controller
    {
        private ContentfriendshipService contentfriendshipService;
        private DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        public ContentfriendshipController(ContentfriendshipService _contentfriendshipService, DatabaseContext _db, IWebHostEnvironment _webHostEnvironment)
        {
            contentfriendshipService = _contentfriendshipService;
            this.db = _db;
            this.webHostEnvironment = _webHostEnvironment;
        }   
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] Content content)
        {
            try
            {
                return Ok(new
                {
                    status = contentfriendshipService.Create(content)
                });
            }
            catch
            {
                return BadRequest();
            }
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
                var content = JsonConvert.DeserializeObject<Content>(strProduct);

                // Ensure product is not null
                if (content == null)    
                {
                    return BadRequest(new { error = "Invalid product data" });
                }

                // Set product photo file name
                content.ContentPhoto = fileName;

                // Create product using service
                var result = contentfriendshipService.Create(content);

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


                return Ok(contentfriendshipService.findByUserid(userid));


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


                return Ok(contentfriendshipService.findByUserid1(userid));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByContentId/{contentid}")]
        public IActionResult findByContentId(int contentid)
        {
            try
            {


                return Ok(contentfriendshipService.findByContentId(contentid));


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
                    status = contentfriendshipService.Remove(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
