using MangXaHoi.Models;
using System.Linq;

namespace MangXaHoi.Services
{
    public class MessageServiceImpl : MessageService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public MessageServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public List<dynamic> findbyfullname(int userId, string fullname)
        {
            var userContent = db.Messages
                .Where(m => (m.UseridguiNavigation.Fullname.Contains(fullname) && m.Useridgui != userId) || (m.UseridnhanNavigation.Fullname.Contains(fullname) && m.Useridnhan != userId)) // Fixed closing parenthesis for Where clause
                .Select(m => new
                {
                    MessageId = m.Id,
                    UserId = userId,
                    OtherUserId = m.Useridgui == userId ? m.Useridnhan : m.Useridgui,
                    UserPhoto = m.Useridgui == userId ? $"{configuration["BaseUrl"]}images/{m.UseridguiNavigation.Photo}" : $"{configuration["BaseUrl"]}images/{m.UseridnhanNavigation.Photo}",
                    OtherUserPhoto = m.Useridgui == userId ? $"{configuration["BaseUrl"]}images/{m.UseridnhanNavigation.Photo}" : $"{configuration["BaseUrl"]}images/{m.UseridguiNavigation.Photo}",
                    ContentPhoto = $"{configuration["BaseUrl"]}images/{m.ContentPhoto}",
                    Date = m.Date,
                    FullName = m.Useridgui == userId ? m.UseridguiNavigation.Fullname : m.UseridnhanNavigation.Fullname,
                    OtherFullName = m.Useridgui == userId ? m.UseridnhanNavigation.Fullname : m.UseridguiNavigation.Fullname,
                })
                .ToList<dynamic>(); // Convert to List<dynamic>

            return userContent;
        }
        public List<dynamic> findbyfullname1(string fullname)
        {
            var userContent = db.Messages
                .Where(m => m.UseridguiNavigation.Fullname.Contains(fullname)) // Fixed closing parenthesis for Where clause
                .Select(m => new
                {
                    MessageId = m.Id,
                    OtherUserId = m.Useridgui == null ? m.Useridnhan : m.Useridgui,
                    UserPhoto = m.Useridgui == null ? $"{configuration["BaseUrl"]}images/{m.UseridguiNavigation.Photo}" : $"{configuration["BaseUrl"]}images/{m.UseridnhanNavigation.Photo}",
                    OtherUserPhoto = m.Useridgui == null ? $"{configuration["BaseUrl"]}images/{m.UseridnhanNavigation.Photo}" : $"{configuration["BaseUrl"]}images/{m.UseridguiNavigation.Photo}",
                    ContentPhoto = $"{configuration["BaseUrl"]}images/{m.ContentPhoto}",
                    Date = m.Date,
                    FullName = m.Useridgui == null ? m.UseridguiNavigation.Fullname : m.UseridnhanNavigation.Fullname,
                    OtherFullName = m.Useridgui == null ? m.UseridnhanNavigation.Fullname : m.UseridguiNavigation.Fullname,
                })
                .ToList<dynamic>(); // Convert to List<dynamic>

            return userContent;
        }
        public bool Create(Message message)
        {
            try
            {
                if (message == null)
                {
                    throw new ArgumentNullException(nameof(message));
                }

                // Kiểm tra trùng lặp
                var existingMessage = db.Messages
                    .FirstOrDefault(m => m.Useridgui == message.Useridgui
                                      && m.Useridnhan == message.Useridnhan);

                if (existingMessage != null)
                {
                    // Tin nhắn đã tồn tại, không thêm mới
                    return false;
                }

                // Nếu không trùng lặp, thêm tin nhắn mới
                message.Status = false;
                message.Date = DateTime.Now;
                db.Messages.Add(message);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        /* public List<dynamic> findByUserId(int userId)
         {
             var userContent = db.Messages
                 .Where(m => m.Useridgui == userId)
                 .Select(m => new
                 {
                     MessageId = m.Id,
                     UserId = userId,
                     OtherUserId = m.Useridgui == userId ? m.Useridnhan : m.Useridgui,
                     UserPhoto = m.Useridgui == userId ? configuration["BaseUrl"] + "images/" + m.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + m.UseridnhanNavigation.Photo,
                     OtherUserPhoto = m.Useridgui == userId ? configuration["BaseUrl"] + "images/" + m.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + m.UseridguiNavigation.Photo,
                     ContentPhoto = configuration["BaseUrl"] + "images/" + m.ContentPhoto,
                     Date = m.Date,
                     FullName = m.Useridgui == userId ? m.UseridguiNavigation.Fullname : m.UseridnhanNavigation.Fullname,
                     OtherFullName = m.Useridgui == userId ? m.UseridnhanNavigation.Fullname : m.UseridguiNavigation.Fullname,
                     MessageType = "Message",
                     Content = m.Chats
                                 .Where(chat => chat.Messagesid == m.Id)
                                 .Select(chat => chat.Content)
                                 .FirstOrDefault()
                 });

             var friendsContent1 = db.Messages
                 .Join(db.Friendships,
                       m => m.Useridgui,
                       f => f.Userid1,
                       (m, f) => new
                       {
                           MessageId = m.Id,
                           Message = m,
                           Friendship = f
                       })
                 .Where(cf => cf.Friendship.Userid2 == userId)
                 .Select(cf => new
                 {
                     MessageId = cf.Message.Id,
                     UserId = userId,
                     OtherUserId = cf.Message.Useridgui == userId ? cf.Message.Useridnhan : cf.Message.Useridgui,
                     UserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo,
                     OtherUserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo,
                     ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Message.ContentPhoto,
                     Date = cf.Message.Date,
                     FullName = userId == cf.Message.Useridgui ? cf.Message.UseridguiNavigation.Fullname : cf.Message.UseridnhanNavigation.Fullname,
                     OtherFullName = userId == cf.Message.Useridgui ? cf.Message.UseridnhanNavigation.Fullname : cf.Message.UseridguiNavigation.Fullname,
                     MessageType = "Message",
                     Content = cf.Message.Chats
                                 .Where(chat => chat.Messagesid == cf.Message.Id)
                                 .Select(chat => chat.Content)
                                 .FirstOrDefault()
                 });

             var friendsContent2 = db.Messages
                 .Join(db.Friendships,
                       m => m.Useridgui,
                       f => f.Userid2,
                       (m, f) => new
                       {
                           MessageId = m.Id,
                           Message = m,
                           Friendship = f
                       })
                 .Where(cf => cf.Friendship.Userid1 == userId)
                 .Select(cf => new
                 {
                     MessageId = cf.Message.Id,
                     UserId = userId,
                     OtherUserId = cf.Message.Useridgui == userId ? cf.Message.Useridnhan : cf.Message.Useridgui,
                     UserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo,
                     OtherUserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo,
                     ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Message.ContentPhoto,
                     Date = cf.Message.Date,
                     FullName = userId == cf.Message.Useridgui ? cf.Message.UseridguiNavigation.Fullname : cf.Message.UseridnhanNavigation.Fullname,
                     OtherFullName = userId == cf.Message.Useridgui ? cf.Message.UseridnhanNavigation.Fullname : cf.Message.UseridguiNavigation.Fullname,
                     MessageType = "Message",
                     Content = cf.Message.Chats
                                 .Where(chat => chat.Messagesid == cf.Message.Id)
                                 .Select(chat => chat.Content)
                                 .FirstOrDefault()
                 });

             var combinedContent = userContent
                 .Concat(friendsContent1)
                 .Concat(friendsContent2)
                 .OrderByDescending(c => c.Date)
                 .ToList();

             return combinedContent.Cast<dynamic>().ToList();
         }*/
        public List<dynamic> findByUserId(int userId)
        {
            var userContent = db.Messages
                .Where(m => m.Useridgui == userId && m.Useridgui != m.Useridnhan)
                .Select(m => new
                {
                    MessageId = m.Id,
                    UserId = userId,
                    OtherUserId = m.Useridgui == userId ? m.Useridnhan : m.Useridgui,
                    UserPhoto = m.Useridgui == userId ? configuration["BaseUrl"] + "images/" + m.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + m.UseridnhanNavigation.Photo,
                    OtherUserPhoto = m.Useridgui == userId ? configuration["BaseUrl"] + "images/" + m.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + m.UseridguiNavigation.Photo,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + m.ContentPhoto,
                    Date = m.Date,
                    FullName = m.Useridgui == userId ? m.UseridguiNavigation.Fullname : m.UseridnhanNavigation.Fullname,
                    OtherFullName = m.Useridgui == userId ? m.UseridnhanNavigation.Fullname : m.UseridguiNavigation.Fullname,
                    MessageType = "Message",
                    Content = m.Chats
                                .Where(chat => chat.Messagesid == m.Id)
                                .Select(chat => chat.Content)
                                .FirstOrDefault()
                });

            var friendsContent1 = db.Messages
                .Join(db.Friendships,
                      m => m.Useridgui,
                      f => f.Userid1,
                      (m, f) => new
                      {
                          MessageId = m.Id,
                          Message = m,
                          Friendship = f
                      })
                .Where(cf => cf.Friendship.Userid2 == userId && cf.Message.Useridgui != cf.Message.Useridnhan)
                .Select(cf => new
                {
                    MessageId = cf.Message.Id,
                    UserId = userId,
                    OtherUserId = cf.Message.Useridgui == userId ? cf.Message.Useridnhan : cf.Message.Useridgui,
                    UserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo,
                    OtherUserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Message.ContentPhoto,
                    Date = cf.Message.Date,
                    FullName = userId == cf.Message.Useridgui ? cf.Message.UseridguiNavigation.Fullname : cf.Message.UseridnhanNavigation.Fullname,
                    OtherFullName = userId == cf.Message.Useridgui ? cf.Message.UseridnhanNavigation.Fullname : cf.Message.UseridguiNavigation.Fullname,
                    MessageType = "Message",
                    Content = cf.Message.Chats
                                .Where(chat => chat.Messagesid == cf.Message.Id)
                                .Select(chat => chat.Content)
                                .FirstOrDefault()
                });

            var friendsContent2 = db.Messages
                .Join(db.Friendships,
                      m => m.Useridgui,
                      f => f.Userid2,
                      (m, f) => new
                      {
                          MessageId = m.Id,
                          Message = m,
                          Friendship = f
                      })
                .Where(cf => cf.Friendship.Userid1 == userId && cf.Message.Useridgui != cf.Message.Useridnhan)
                .Select(cf => new
                {
                    MessageId = cf.Message.Id,
                    UserId = userId,
                    OtherUserId = cf.Message.Useridgui == userId ? cf.Message.Useridnhan : cf.Message.Useridgui,
                    UserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo,
                    OtherUserPhoto = userId == cf.Message.Useridgui ? configuration["BaseUrl"] + "images/" + cf.Message.UseridnhanNavigation.Photo : configuration["BaseUrl"] + "images/" + cf.Message.UseridguiNavigation.Photo,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Message.ContentPhoto,
                    Date = cf.Message.Date,
                    FullName = userId == cf.Message.Useridgui ? cf.Message.UseridguiNavigation.Fullname : cf.Message.UseridnhanNavigation.Fullname,
                    OtherFullName = userId == cf.Message.Useridgui ? cf.Message.UseridnhanNavigation.Fullname : cf.Message.UseridguiNavigation.Fullname,
                    MessageType = "Message",
                    Content = cf.Message.Chats
                                .Where(chat => chat.Messagesid == cf.Message.Id)
                                .Select(chat => chat.Content)
                                .FirstOrDefault()
                });

            var combinedContent = userContent
                .Concat(friendsContent1)
                .Concat(friendsContent2)
                .OrderByDescending(c => c.Date)
                .GroupBy(c => c.OtherFullName)
                .Select(g => g.First())
                .ToList();

            return combinedContent.Cast<dynamic>().ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                db.Messages.Remove(db.Messages.Find(id));
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
    

