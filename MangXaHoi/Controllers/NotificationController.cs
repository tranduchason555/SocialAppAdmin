
using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MangXaHoi.Controllers
{
    [Route("api/notification")]
    public class NotificationController : Controller
    {
        private NotificationService notificationService;
        private DatabaseContext db;
        public NotificationController(NotificationService _notificationService, DatabaseContext _db)
        {
            notificationService = _notificationService;
            this.db = _db;
        }
        [Produces("application/json")]
        [Route("notificationFriendshipId/{userid}")]
        public IActionResult notificationFriendshipId(int userid)
        {
            try
            {


                return Ok(notificationService.NotificationFriendshipId(userid));


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
                    status = notificationService.Remove(id)
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
