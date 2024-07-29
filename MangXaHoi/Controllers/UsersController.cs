
using MangXaHoi.Helpers;
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DoAnMangXaHoi.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private UserService userService;
        private DatabaseContext db;
        IWebHostEnvironment webHostEnvironment;
        public UsersController(UserService _userService, DatabaseContext _db, IWebHostEnvironment _webHostEnvironment)
        {
            userService = _userService;
            this.db = _db;
            this.webHostEnvironment = _webHostEnvironment;
        }
        [Produces("application/json")]
        [Route("findAll/{userid}")]
        public IActionResult FindAll(int userid)
        {
            try
            {
                return Ok(userService.findAll(userid));
            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        //CẦN XÁC ĐỊNH DỮ LIỆU WEB API ĐỔ RA LÀ DỮ LIỆU GÌ
        [Produces("application/json")]
        //BIẾN ĐỐI TƯỢNG JSON THÀNH PRODUCT
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var k = db.Users.SingleOrDefault(x => x.Email == user.Email );
                if (k != null && BCrypt.Net.BCrypt.Verify(user.Password, k.Password))
                {
                    return Ok(new
                    {
                        status = true
                    });

                }
                else
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

            }
            catch (Exception ex)
            {
                //lời gọi thất bại
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("create")]
        //Biến kiểu json thành kiểu đối tượng
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                return Ok(new
                {
                    status = userService.Create(user)
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
               
                
                    return Ok(userService.findByUserid(userid));
                

            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByFullname/{fullname}")]
        public IActionResult findByFullname(string fullname)
        {
            try
            {


                return Ok(userService.findByFullname(fullname));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByEmail/{email}")]
        public IActionResult findByEmail(string email)
        {
            try
            {


                return Ok(userService.findByEmail(email));


            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [Route("findByEmail1/{email}/{userid}")]
        public IActionResult findByEmail1(string email,int userid)
        {
            try
            {


                return Ok(userService.findByEmail1(email,userid));


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
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                return Ok(new
                {
                    status = userService.Update(user)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        [HttpPost("update2")]
        public IActionResult Update2(IFormFile file, string strProduct)
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
                var user = JsonConvert.DeserializeObject<User>(strProduct);

                // Ensure product is not null
                if (user == null)
                {
                    return BadRequest(new { error = "Invalid product data" });
                }

                // Set product photo file name
                user.Photo = fileName;

                // Create product using service
                var result = userService.Update(user);

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
    }
}
