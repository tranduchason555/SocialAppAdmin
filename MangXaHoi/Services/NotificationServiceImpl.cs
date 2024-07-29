
using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class NotificationServiceImpl : NotificationService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public NotificationServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public bool Remove(int id)
        {
            try
            {
                db.Notifications.Remove(db.Notifications.Find(id));
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<dynamic> NotificationFriendshipId(int userId)
        {
            var friendsNotification1 = db.Notifications
                .Join(db.Friendships,
                    s => s.Userid,
                    f => f.Userid1,
                    (s, f) => new { Notification = s, Friendship = f })
                .Where(sf => sf.Friendship.Userid2 == userId)
                .Select(sf => new
                {
                    Id = sf.Notification.Id,
                    ContentId = sf.Notification.Contentid,
                    DateContent = sf.Notification.Content != null ? sf.Notification.Content.Date : (DateTime?)null,
                    StoryId = sf.Notification.Storyid,
                    DateStory = sf.Notification.Story != null ? sf.Notification.Story.Date : (DateTime?)null,
                    LikeId = sf.Notification.Likeid,
                    DateLike = sf.Notification.Like != null ? sf.Notification.Like.Date : (DateTime?)null,
                    Status = sf.Notification.Status,
                    UserId = sf.Notification.Userid,
                    UserPhoto = configuration["BaseUrl"] + "images/" + sf.Notification.User.Photo,
                    FriendshipId = sf.Notification.Friendshipid,
                    DateFriendship = sf.Notification.Friendship != null ? sf.Notification.Friendship.Date : (DateTime?)null,
                    MessageId = sf.Notification.Messageid,
                    DateMessage = sf.Notification.Message.Chats != null ? sf.Notification.Message.Chats.FirstOrDefault().Date : (DateTime?)null,
                    CommentId = sf.Notification.Commentid,
                    DateComment = sf.Notification.Comment != null ? sf.Notification.Comment.Date : (DateTime?)null,
                    Fullname = sf.Notification.User.Fullname,
                    StatusFriendship=sf.Friendship.Status
                })
                .ToList();

            var friendsNotification2 = db.Notifications
                .Join(db.Friendships,
                    s => s.Userid,
                    f => f.Userid2,
                    (s, f) => new { Notification = s, Friendship = f })
                .Where(sf => sf.Friendship.Userid1 == userId)
                .Select(sf => new
                {
                    Id = sf.Notification.Id,
                    ContentId = sf.Notification.Contentid,
                    DateContent = sf.Notification.Content != null ? sf.Notification.Content.Date : (DateTime?)null,
                    StoryId = sf.Notification.Storyid,
                    DateStory = sf.Notification.Story != null ? sf.Notification.Story.Date : (DateTime?)null,
                    LikeId = sf.Notification.Likeid,
                    DateLike = sf.Notification.Like != null ? sf.Notification.Like.Date : (DateTime?)null,
                    Status = sf.Notification.Status,
                    UserId = sf.Notification.Userid,
                    UserPhoto = configuration["BaseUrl"] + "images/" + sf.Notification.User.Photo,
                    FriendshipId = sf.Notification.Friendshipid,
                    DateFriendship = sf.Notification.Friendship != null ? sf.Notification.Friendship.Date : (DateTime?)null,
                    MessageId = sf.Notification.Messageid,
                    DateMessage = sf.Notification.Message.Chats != null ? sf.Notification.Message.Chats.FirstOrDefault().Date : (DateTime?)null,
                    CommentId = sf.Notification.Commentid,
                    DateComment = sf.Notification.Comment != null ? sf.Notification.Comment.Date : (DateTime?)null,
                    Fullname = sf.Notification.User.Fullname,
                    StatusFriendship = sf.Friendship.Status
                })
                .ToList();

            var combinedContent = friendsNotification1
                .Concat(friendsNotification2)
                .OrderByDescending(n => n.Id) // Sắp xếp theo ID giảm dần để cái mới nhất ở đầu
                .Cast<dynamic>()
                .ToList();

            return combinedContent;
        }

    }
}
