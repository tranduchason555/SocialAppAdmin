using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class StoryfriendshipServiceImpl : StoryfriendshipService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public StoryfriendshipServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public bool Create(Story story)
        {
            try
            {
                if (story == null)
                {
                    throw new ArgumentNullException(nameof(story));
                }
                story.Status = true;
                story.Date = DateTime.Now;
                db.Stories.Add(story);

                if (db.SaveChanges() > 0)
                {
                    Notification notification = new Notification
                    {
                        Storyid = story.Id,
                        Status = false
                    };
                    db.Notifications.Add(notification);
                    return db.SaveChanges() > 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<dynamic> findByUserid(int userid)
        {
            var userStory = db.Stories
                .Where(s => s.Userid == userid)
                .Select(s => new
                {
                    Id = s.Id,
                    Content = s.Content,
                    Photo = configuration["BaseUrl"] + "images/" + s.Photo,
                    Date = s.Date,
                    Fullname = s.User.Fullname,
                    PhotoUser = configuration["BaseUrl"] + "images/" + s.User.Photo,
                    UserId = s.Userid,
                    Status=s.Status,
                });

            var friendsStory1 = db.Stories
                .Join(db.Friendships,
                      s => s.Userid,
                      f => f.Userid1,
                      (s, f) => new { Story = s, Friendship = f })
                .Where(sf => sf.Friendship.Userid2 == userid)
                .Select(sf => new
                {
                    Id = sf.Story.Id,
                    Content = sf.Story.Content,
                    Photo = configuration["BaseUrl"] + "images/" + sf.Story.Photo,
                    Date = sf.Story.Date,
                    Fullname = sf.Story.User.Fullname,
                    PhotoUser = configuration["BaseUrl"] + "images/" + sf.Story.User.Photo,
                    UserId = sf.Story.Userid,
                    Status = sf.Story.Status,
                });

            var friendsStory2 = db.Stories
                .Join(db.Friendships,
                      s => s.Userid,
                      f => f.Userid2,
                      (s, f) => new { Story = s, Friendship = f })
                .Where(sf => sf.Friendship.Userid1 == userid)
                .Select(sf => new
                {
                    Id = sf.Story.Id,
                    Content = sf.Story.Content,
                    Photo = configuration["BaseUrl"] + "images/" + sf.Story.Photo,
                    Date = sf.Story.Date,
                    Fullname = sf.Story.User.Fullname,
                    PhotoUser = configuration["BaseUrl"] + "images/" + sf.Story.User.Photo,
                    UserId = sf.Story.Userid,
                    Status = sf.Story.Status,
                });

            var combinedContent = userStory.ToList()
                .Concat(friendsStory1.ToList())
                .Concat(friendsStory2.ToList());

            return combinedContent
                .OrderByDescending(c => c.Date)
                .Cast<dynamic>()
                .ToList();
        }
        public List<dynamic> findByUserid1(int userid)
        {
            return db.Stories
                      .Where(p => p.Userid == userid)
                      .Select(p => new
                      {
                          Id = p.Id,
                          Content = p.Content,
                          Userid = p.Userid,
                          PhotoUser = configuration["BaseUrl"] + "images/" +p.User.Photo,
                          Date = p.Date,
                          Fullname = p.User.Fullname,
                          Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                          Status = p.Status,
                      })
                      .ToList<dynamic>(); // Convert to List<dynamic>
        }
    }
}
