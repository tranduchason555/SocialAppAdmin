using MangXaHoi.Helpers;
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("admin")]
    [Route("")]
    public class AdminController : Controller
    {
        private UserService userService;
        private ReportService reportService;
        private IWebHostEnvironment webHostEnvironment;
        public AdminController(UserService _userService, ReportService _reportService, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = _userService;
            this.reportService = _reportService;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Route("login")]
        [Route("")]
        //     [Route("~/")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            try
            {
                // Kiểm tra xem user tồn tại trong database
                var check = userService.findByEmail(email);

                if (check != null) // Kiểm tra xem user có tồn tại
                {
                    // Kiểm tra password
                    if (BCrypt.Net.BCrypt.Verify(password, check.Password))
                    {
                        // Lưu fullname vào session
                       // HttpContext.Session.SetString("fullname", check.Fullname);
                       string fullname=check.Fullname;
                        string img = check.Photo;
                        HttpContext.Session.SetString("fullname", fullname);
                        HttpContext.Session.SetString("img", img);
                        // Kiểm tra quyền của user
                        if (check.RoleId == 1) // Assuming 1 is the admin role
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "User");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "Mật khẩu không đúng.";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["msg"] = "Tài khoản không tồn tại.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Lỗi hệ thống: " + ex.Message;
                return RedirectToAction("Login");
            }
        }
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.dsadmin = userService.findAllAdmin();
            ViewBag.dsuser = userService.findAllUser();
            ViewBag.dsbaiviet = reportService.findAll();
            return View();
        }
        [Route("quanlytaikhoan")]
        public IActionResult Quanlytaikhoan()
        {
            ViewBag.dsuser = userService.findAllUser();
            return View();
        }
        [Route("addquanlytaikhoan")]
        public IActionResult Addquanlytaikhoan()
        {
            return View();
        }
        [HttpPost]
        [Route("addquanlytaikhoan")]
        public IActionResult AddBatDongSan(User user, IFormFile file)
        {
            if (file != null)
            {
                //Lấy tên file ng ta uplaod lên
                var fileName = FileHelper.generateFileName(file.FileName);
    
                //Khai báo đường dẫn, lấy đường dẫn
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                //Lấy dường dẫn về wwroot
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                user.Photo = fileName;
            }
            else
            {
                user.Photo = "noimage.png";
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            if (userService.CreateAdmin(user))
            {
                TempData["msg"] = "Done";
            }
            else
            {
                TempData["msg"] = "Failed";
            }
            return RedirectToAction("Index");
        }
        [Route("quanlybaiviet")]
        public IActionResult Quanlybaiviet()
        {
            ViewBag.dsbaiviet = reportService.findAll();
            return View();
        }
        [Route("removebaiviet/{id}")]
        public IActionResult RemoveBaiViet(int id)
        {
            if (reportService.Delete(id))
            {
                TempData["msg"] = "Done";
            }
            else
            {
                TempData["msg"] = "Failed";
            }
            return RedirectToAction("Quanlybaiviet");
        }
        [Route("removetaikhoan/{id}")]
        public IActionResult RemoveTaiKhoan(int id)
        {
            if (userService.Remove(id))
            {
                TempData["msg"] = "Done";
            }
            else
            {
                TempData["msg"] = "Failed";
            }
            return RedirectToAction("Index");
        }
    }
}
