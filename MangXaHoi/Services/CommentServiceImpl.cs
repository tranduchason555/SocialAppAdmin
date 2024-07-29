using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class CommentServiceImpl : CommentService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public CommentServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

        public bool Create(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    throw new ArgumentNullException(nameof(comment));
                }
           /*     var contentUserId = db.Contents
     .Where(c => c.Id == comment.Contentid)
     .Select(c => c.Userid)  // Assuming you want the UserId associated with the content
     .FirstOrDefault();*/
                comment.Date = DateTime.Now;
                db.Comments.Add(comment);

                if (db.SaveChanges() > 0)
                {
                    Notification notification = new Notification
                    {
                       
                        Commentid = comment.Id,
                        Status = false,
                        Userid= comment.Userid,
                    };
                    db.Notifications.Add(notification);
                    return db.SaveChanges() > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<dynamic> findByContentid(int contentid)
        {
            return db.Comments
                      .Where(p => p.Contentid==contentid)
                      .Select(p => new
                      {
                         Id=p.Id,
                         Comment1= p.Comment1,
                         Userid=p.User.Id,
                         Contentid=p.Contentid,
                         Date=p.Date,
                         Fullname=p.User.Fullname,
                         Photo= configuration["BaseUrl"] + "images/" + p.User.Photo,
                      })
                      .ToList<dynamic>(); // Convert to List<dynamic>
        }

        public List<dynamic> findByMessageid(int messageid)
        {
            return db.Chats
                      .Where(p => p.Messages.Id == messageid)
                      .Select(p => new
                      {
                          Id = p.Id,
                          Content = p.Content,
                          Userid = p.User.Id,
                          Messagesid = p.Messagesid,
                          Date = p.Date,
                      })
                      .ToList<dynamic>(); // Convert to List<dynamic>
        }
    }
}
